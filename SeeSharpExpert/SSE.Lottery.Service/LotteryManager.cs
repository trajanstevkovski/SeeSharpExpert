using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.Lottery.View.Model;
using System.Data.Entity;
using SSE.Lotery.Data.Model;
using SSE.Lottery.Data;
using SSE.Lottery.Service.UoW;
using SSE.Lottery.Mapper;

namespace SSE.Lottery.Service
{
    public class LotteryManager : ILotteryManager
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;

        public LotteryManager(IRepository<Code> codeRepository,
            IRepository<Award> awardRepository,
            IRepository<UserCode> userCodeRepository,
            IRepository<UserCodeAward> userCodeAwardRepository,
            DbContext dbContext)
        {
            _codeRepository = codeRepository;
            _awardRepository = awardRepository;
            _userCodeRepository = userCodeRepository;
            _userCodeAwardRepository = userCodeAwardRepository;
            _dbContext = dbContext;
        }

        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            using(var uow = new UnitOfWork(_dbContext))
            {
                var code = _codeRepository.GetAll().FirstOrDefault(x => x.CodeValue == userCodeModel.Code.CodeValue);

                if (code == null)
                    throw new ApplicationException("Invalid code.");

                if (code.IsUsed)
                    throw new ApplicationException("Code is used.");

                var userCode = new UserCode
                {
                    Code = code,
                    Email = userCodeModel.Email,
                    FirstName = userCodeModel.FirstName,
                    LastName = userCodeModel.LastName,
                    SentAt = DateTime.Now
                };

                _userCodeRepository.Insert(userCode);

                Award award = null;
                if (code.IsWinning)
                {
                    award = GetRandomAward(RuffledType.Immediate);

                    var userCodeAward = new UserCodeAward
                    {
                        Award = award,
                        UserCode = userCode,
                        WonAt = DateTime.Now
                    };

                    _userCodeAwardRepository.Insert(userCodeAward);
                }

                code.IsUsed = true;

                uow.Commit();

                return award?.Map<Award, AwardModel>();
            }
        }

        private Award GetRandomAward(RuffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RuffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RuffledType == (byte)type)
                .Select(x => x.Award)
                .GroupBy(x => x.Id)
                .ToList();

            var avalibleAwards = new List<Award>();

            foreach (var award in awards)
            {
                var numberOfAwardedAwards = awardedAwards
                    .FirstOrDefault(x => x.Key == award.Id)?.Count() ?? 0;
                var awardsLeft = award.Quantaty - numberOfAwardedAwards;
                avalibleAwards.AddRange(Enumerable.Repeat(award, awardsLeft));
            }

            if (avalibleAwards.Count == 0)
                throw new ApplicationException("We are out of awards. Sorry!");

            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, avalibleAwards.Count);
            return avalibleAwards[randomAwardIndex];
        }
    }
}
