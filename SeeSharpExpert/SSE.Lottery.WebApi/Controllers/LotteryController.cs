using SSE.Lottery.Service;
using SSE.Lottery.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSE.Lottery.WebApi.Controllers
{
    public class LotteryController : ApiController
    {
        private readonly ILotteryManager _loteryManager;

        public LotteryController(ILotteryManager lotteryManager)
        {
            _loteryManager = lotteryManager;
        }

        [HttpPost]
        public AwardModel SubmitCode([FromBody]UserCodeModel userCodeModel)
        {
            return _loteryManager.CheckCode(userCodeModel);
        }
    }
}
