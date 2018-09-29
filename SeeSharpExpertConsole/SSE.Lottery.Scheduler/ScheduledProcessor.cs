using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;

namespace SSE.Lottery.Scheduler
{
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        protected abstract string Schedule { get; }

        public ScheduledProcessor(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            do
            {
                var now = DateTime.Now;
                if(now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            } while (!token.IsCancellationRequested);
        }
    }
}
