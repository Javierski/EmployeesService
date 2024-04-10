using Domain.Entity;

namespace Domain.Ports
{
    public interface IOvertimeRepository
    {
        Task<IEnumerable<Overtime>> GetAllOvertimeAsync();
        Task<IEnumerable<Overtime>> GetAllByIdOvertimeAsync(int id);
        Task<Overtime?> GetByIdAsync(int id);
        Task<Overtime> AddOvertimeAsync(Overtime overtime);
        Task UpdateOvertimeAsync(Overtime overTime);
    }
}