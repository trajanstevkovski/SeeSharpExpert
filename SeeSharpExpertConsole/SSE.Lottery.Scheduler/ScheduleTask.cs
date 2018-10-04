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
        private readonly ICodesManager _codesManager;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory,
            ILotteryManager lotteryManager,
            ICodesManager codesManager) : base(serviceScopeFactory)
        {
            _codesManager = codesManager;
            _lotteryManager = lotteryManager;
        }

        protected override string Schedule => "* * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            try
            {
                //Console.WriteLine($"Raffle start at: {DateTime.Now}");
                //_lotteryManager.Raffle();
                //Console.WriteLine($"Raffle finished at: {DateTime.Now}");
                Console.WriteLine($"Codes Procesing start at: {DateTime.Now}");
                _codesManager.ProcesCodes();
                Console.WriteLine($"Codes Procesing ends at: {DateTime.Now}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
