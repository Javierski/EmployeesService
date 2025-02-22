﻿using Domain.Entity;

namespace Domain.Ports
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employ);
        Task UpdateEmployeeAsync(Employee employ);
        Task DeleteEmployeeAsync(int id);
    }
}
