using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SSE.Lottery.RaffleService;

namespace SSE.Lottery.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ILotteryManager _lotteryManager;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ILotteryManager lotteryManager) : base(serviceScopeFactory)
        {
            _lotteryManager = lotteryManager;
        }

        protected override string Schedule => "* * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            try
            {
                Console.WriteLine($"Raffle start at: {DateTime.Now}");
                _lotteryManager.Raffle();
                Console.WriteLine($"Raffle finished at: {DateTime.Now}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
