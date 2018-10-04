using OfficeOpenXml;
using SSE.Lottery.RaffleData;
using SSE.Lottery.RaffleData.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SSE.Lottery.RaffleService
{
    public class LocalCodesManager : ICodesManager
    {
        private readonly IExcelManager _excelManager;

        public LocalCodesManager(IExcelManager excelManager)
        {
            _excelManager = excelManager;
        }

        public void ProcesCodes()
        {
            var folderName = "LocalStorage\\";
            var folderPath = $@"{Directory.GetCurrentDirectory()}\{folderName}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var directoryInfo = new DirectoryInfo(folderPath);
            var files = directoryInfo.GetFiles("*.xlsx");

            foreach (var file in files)
            {
                _excelManager.ProcessExcelPackage(file);
                File.Delete(file.FullName);
            }
        }
    }
}
