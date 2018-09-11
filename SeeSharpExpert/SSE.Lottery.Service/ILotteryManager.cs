using SSE.Lottery.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Lottery.Service
{
    public interface ILotteryManager
    {
        AwardModel CheckCode(UserCodeModel userCode);
        List<UserCodeAwardModel> GetAllWinners();
    }
}
