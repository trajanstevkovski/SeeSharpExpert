﻿using SSE.Lottery.Service;
using SSE.Lottery.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SSE.Lottery.WebApi.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
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

        [HttpGet]
        public List<UserCodeAwardModel> AllWinners()
        {
            return _loteryManager.GetAllWinners();
        }
    }
}
