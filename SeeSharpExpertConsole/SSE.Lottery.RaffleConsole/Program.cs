using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSE.Lottery.RaffleData;
using SSE.Lottery.RaffleData.Model;
using SSE.Lottery.RaffleService;
using SSE.Lottery.Scheduler;
using System;
using System.IO;
using System.Threading;

namespace SSE.Lottery.RaffleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Configure();
            

            var cronScheduler = serviceProvider.GetService<IHostedService>();
            cronScheduler.StartAsync(new CancellationToken());

            while (true)
            {

            }


        }

        static IServiceProvider Configure()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(provider => configuration)
                .AddSingleton<DbContext, LotteryContext>()
                .AddSingleton<ILotteryManager, LotteryManager>()
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<IHostedService, ScheduleTask>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
