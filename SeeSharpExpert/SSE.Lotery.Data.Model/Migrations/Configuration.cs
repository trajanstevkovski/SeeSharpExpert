using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace SSE.Lotery.Data.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LotteryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        //protected override void Seed(LotteryContext context)
        //{
        //    var codes = new List<Code>
        //    {
        //        new Code { CodeValue = "CC8899", IsWinning = true },
        //        new Code { CodeValue = "CC7799", IsWinning = false },
        //        new Code { CodeValue = "CC6699", IsWinning = false },
        //        new Code { CodeValue = "CC5599", IsWinning = true }
        //    };

        //    context.Codes.AddRange(codes);

        //    var awards = new List<Award>
        //    {
        //        new Award { AwardName = "Beer", AwardDescription = "You won a beer", Quantaty = 100, RuffledType = (byte) RuffledType.Immediate},
        //        new Award { AwardName = "iPhoneX", AwardDescription = "You won an iPhoneX", Quantaty = 2, RuffledType = (byte) RuffledType.PerDay},
        //        new Award { AwardName = "VW Polo", AwardDescription = "You won a Polo", Quantaty = 1, RuffledType = (byte) RuffledType.Final}
        //    };

        //    context.Awards.AddRange(awards);

        //    context.SaveChanges();
        //}
    }
}
