using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SSE.Lottery.RaffleService
{
    public interface IExcelManager
    {
        void ProcessExcelPackage(FileInfo fileInfo);
        void ProcessExcelPackage(Stream fileStream);
    }
}
