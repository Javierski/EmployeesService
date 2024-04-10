using Application.Interfaces;
using Domain.Entity;
using Domain.Ports;

namespace Application.Services
{
    public class OvertimeService : IOvertimeService
    {
        private readonly IOvertimeRepository _overtimeRepository;
        public OvertimeService(IOvertimeRepository overtimeRepository)
        {
            _overtimeRepository = overtimeRepository;
        }

        public async Task<IEnumerable<Overtime>> GetAllOvertimeAsync()
        {
            try
            {
                return await _overtimeRepository.GetAllOvertimeAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckLimit(int employeeId)
        {
            var overtimes = await _overtimeRepository.GetAllByIdOvertimeAsync(employeeId);
            var totalhours = overtimes.Sum(s => s.Hours);
            return totalhours > 40;
        }

        public async Task<Overtime> AddOvertimeAsync(Overtime overtime)
        {
            try
            {
                if (CheckLimit(overtime.EmployeeId).Result)
                {
                    return await _overtimeRepository.AddOvertimeAsync(overtime);
                }

                return new Overtime();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task ApproveAsync(int id, int approverId)
        {
            try
            {
                var overtimeRequest = await _overtimeRepository.GetByIdAsync(id);

                if (overtimeRequest == null)
                {
                    // La solicitud de horas extras no existe
                    return;
                }

                var state = "Pendiente";
                var overtimeNew = new Overtime()
                {
                    //Id = id,
                    //State = state
                };

                await _overtimeRepository.UpdateOvertimeAsync(overtimeNew);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task RejectAsync(int id)
        {
            try
            {
                var state = "Rechazado";
                var overtimeNew = new Overtime()
                {
                    //Id = id,
                    //State = state
                };

                await _overtimeRepository.UpdateOvertimeAsync(overtimeNew);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task ConfirmAsync(int id)
        {
            try
            {
                var state = "Aprobado";
                var overtimeNew = new Overtime()
                {
                    //Id = id,
                    //State = state
                };

                await _overtimeRepository.UpdateOvertimeAsync(overtimeNew);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}