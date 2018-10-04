using System;
using SSE.Lottery.RaffleData.Model;
using System.Linq;
using SSE.Lottery.RaffleData;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SSE.Lottery.RaffleService
{
    public class LotteryManager : ILotteryManager
    {
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;
        private readonly IConfigurationRoot _configurationRoot;

        public LotteryManager(IRepository<Award> awardRepository,
            IRepository<UserCodeAward> userCodeAwardRepository,
            IRepository<UserCode> userCodeRepository,
            IConfigurationRoot configurationRoot)
        {
            _userCodeAwardRepository = userCodeAwardRepository;
            _awardRepository = awardRepository;
            _userCodeRepository = userCodeRepository;
            _configurationRoot = configurationRoot;
        }

        public void Raffle()
        {
            var finalRaffle = DateTime.Parse(_configurationRoot.GetSection("FinalRaffle").Value);

            if (DateTime.Now.Day <= finalRaffle.Day)
            {
                GiveAwards(RaffledType.PerDay);
            }
            if (DateTime.Now.Day == finalRaffle.Day)
            {
                GiveAwards(RaffledType.Final);
            }
        }

        public void GiveAwards(RaffledType type)
        {
            var awardsQuantity = GetAwardQuantityPerType(type);
            for (int i = 0; i < awardsQuantity; i++)
            {
                GiveAward(type);
            }
        }

        private int GetAwardQuantityPerType(RaffledType type)
        {
            var awardsQuantity = _awardRepository.GetAll().Where(x => x.RuffledType == (byte)type)
                .Select(x => x.Quantaty).Sum();
            return awardsQuantity;
        }

        private void GiveAward(RaffledType type)
        {
            var users = _userCodeRepository.GetAll().Include(x => x.Code).Where(x => !x.Code.IsWinning);

            if(type == RaffledType.PerDay)
            {
                users = users.Where(x => x.SentAt.Date == DateTime.Now.Date);
            }

            var userList = users.ToList();

            var userCodeAward = _userCodeAwardRepository.GetAll().ToList();

            userList = userList.Where(x => userCodeAward.All(y => y.UserCodeId != x.Id)).ToList();

            if (!userList.Any()) return;

            var rand = new Random();
            var randomIndex = rand.Next(0, userList.Count - 1);
            var winningUser = userList[randomIndex];

            var randomAward = GetRandomAward(type);

            _userCodeAwardRepository.Insert(new UserCodeAward
            {
                Award = randomAward,
                UserCode = winningUser,
                WonAt = DateTime.Now
            });
        }

        private Award GetRandomAward(RaffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RuffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RuffledType == (byte)type);
            if(type == RaffledType.PerDay)
            {
                awardedAwards.Where(x => x.WonAt.Date == DateTime.Now.Date);
            }

            var awardedAwardsGroup = awardedAwards
                .Select(x => x.Award)
                .GroupBy(x => x.Id)
                .ToList();

            
            var avalibleAwards = new List<Award>();

            foreach (var award in awards)
            {
                var numberOfAwardedAwards = awardedAwardsGroup
                    .FirstOrDefault(x => x.Key == award.Id)?.Count() ?? 0;
                var awardsLeft = award.Quantaty - numberOfAwardedAwards;
                avalibleAwards.AddRange(Enumerable.Repeat(award, awardsLeft));
            }

            if (avalibleAwards.Count == 0)
                throw new ApplicationException("We are out of awards. Sorry!");

            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, avalibleAwards.Count - 1);
            return avalibleAwards[randomAwardIndex];
        }
    }
}
