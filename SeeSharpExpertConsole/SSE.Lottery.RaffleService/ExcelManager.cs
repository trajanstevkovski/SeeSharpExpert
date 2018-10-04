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
    public class ExcelManager : IExcelManager
    {
        private readonly IRepository<Code> _codeRepository;

        public ExcelManager(IRepository<Code> repository)
        {
            _codeRepository = repository;
        }

        public void ProcessExcelPackage(FileInfo fileInfo)
        {
            using (var content = new ExcelPackage(fileInfo))
            {
                ProcesExcelPackage(content);
            }
        }

        public void ProcessExcelPackage(Stream fileStream)
        {
            using (var content = new ExcelPackage(fileStream))
            {
                ProcesExcelPackage(content);
            }
        }

        private void ProcesExcelPackage(ExcelPackage content)
        {
            var worksheet = content.Workbook.Worksheets[1];
            var numberOfRows = worksheet.Dimension.Rows;
            for (int i = 1; i <= numberOfRows; i++)
            {
                var code = worksheet.Cells[i, 1].Value.ToString();
                var isWinning = bool.Parse(worksheet.Cells[i, 2].Value.ToString());

                if (!_codeRepository.GetAll().Any(x => x.CodeValue == code))
                {
                    var codeObject = new Code
                    {
                        CodeValue = code,
                        IsUsed = false,
                        IsWinning = isWinning
                    };

                    _codeRepository.Insert(codeObject);
                }
            }
        }
    }
}
