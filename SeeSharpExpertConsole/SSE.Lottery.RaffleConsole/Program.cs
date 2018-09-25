using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSE.Lottery.RaffleData;
using SSE.Lottery.RaffleData.Model;
using SSE.Lottery.RaffleService;
using System;
using System.IO;

namespace SSE.Lottery.RaffleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Configure();
            
            var lotteryManager = serviceProvider.GetService<ILotteryManager>();
            var configuration = serviceProvider.GetService<IConfigurationRoot>();

            var finalRaffle = DateTime.Parse(configuration.GetSection("FinalRaffle").Value);
            if(DateTime.Now.Day <= finalRaffle.Day)
            {
                lotteryManager.GiveAwards(RaffledType.PerDay);
            }
            if(DateTime.Now.Day == finalRaffle.Day)
            {
                lotteryManager.GiveAwards(RaffledType.Final);
            }
        }

        static IServiceProvider Configure()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(provider => configuration)
                .AddSingleton<DbContext, LotteryContext>()
                .AddSingleton<ILotteryManager, LotteryManager>()
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
