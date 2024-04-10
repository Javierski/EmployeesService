using Domain.Dto;

namespace Application.Interfaces
{
    public interface IExcelImportService
    {
        IEnumerable<EmployeeUpload> ImportEmployeesFromExcel(Stream stream);
    }
}