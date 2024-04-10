using Domain.Entity;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class OvertimeRepository : IOvertimeRepository
    {
        private readonly ApiContext _context;
        public OvertimeRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Overtime>> GetAllOvertimeAsync()
        {
            return await _context.Overtimes.ToListAsync();
        }

        public async Task<IEnumerable<Overtime>> GetAllByIdOvertimeAsync(int id)
        {
            return await _context.Overtimes.Where(x => x.EmployeeId == id).Where(m => m.Date.Month == System.DateTime.Now.Month).ToListAsync();
        }

        public async Task<Overtime?> GetByIdAsync(int id)
        {
            return await _context.Overtimes.FindAsync(id);
        }

        public async Task<Overtime> AddOvertimeAsync(Overtime overtime)
        {
            _context.Overtimes.Add(overtime);
            await _context.SaveChangesAsync();
            return overtime;
        }

        public async Task UpdateOvertimeAsync(Overtime overTime)
        {
            _context.Entry(overTime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}