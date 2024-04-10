using Application.Interfaces;
using ClosedXML.Excel;
using Domain.Dto;

namespace Application.Services
{
    public class ExcelImportService : IExcelImportService
    {
        public IEnumerable<EmployeeUpload> ImportEmployeesFromExcel(Stream stream)
        {
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                yield return new EmployeeUpload
                //var load = new EmployeeUpload
                {
                    Area = row.Cell(1).GetValue<string>(),
                    EmployeeName = row.Cell(2).GetValue<string>(),
                    Leader = row.Cell(3).GetValue<string>()
                };


            }
        }
    }
}