using System;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(Guid id);
        Task<bool> CreateEmployeeAsync(Employee employee);
        Task<int> GetEmployeeCountAsync(LevelEnum employeeLevel);
    }
}