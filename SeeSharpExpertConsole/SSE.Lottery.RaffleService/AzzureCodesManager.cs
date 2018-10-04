using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SSE.Lottery.RaffleService
{
    public class AzzureCodesManager : ICodesManager
    {
        private readonly IExcelManager _excelManager;

        public AzzureCodesManager(IExcelManager excelManager)
        {
            _excelManager = excelManager;
        }

        public void ProcesCodes()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("");
            
            using(var stream = client.GetStreamAsync("codes.xlxs").Result)
            {

            }
        }
    }
}
