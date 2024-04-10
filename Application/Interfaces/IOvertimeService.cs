using Domain.Entity;

namespace Application.Interfaces
{
    public interface IOvertimeService
    {
        Task<Overtime> AddOvertimeAsync(Overtime overtime);
        Task<IEnumerable<Overtime>> GetAllOvertimeAsync();
        Task ApproveAsync(int id, int approverId);
        Task RejectAsync(int id);
        Task ConfirmAsync(int id);
    }
}