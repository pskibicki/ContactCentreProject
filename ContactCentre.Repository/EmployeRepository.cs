using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Repository
{
    public class EmployeRepository : IEmployeeRepository
    {
        private static readonly Dictionary<LevelEnum, List<Employee>> Employees = new Dictionary<LevelEnum, List<Employee>>();
        
       public async Task<Employee> GetEmployeeAsync(Guid id)
       {
           return Employees.SelectMany(x => x.Value).FirstOrDefault(x => x.Id == id);
       }

       public async Task<bool> CreateEmployeeAsync(Employee employee)
       {
            if (!Employees.ContainsKey(employee.Level))
            {
               Employees.Add(employee.Level, new List<Employee>());
            }

            Employees[employee.Level].Add(employee);

            return true;
       }

       public async Task<int> GetEmployeeCountAsync(LevelEnum employeeLevel)
       {
           if (!Employees.ContainsKey(employeeLevel))
           {
               return 0;
           }

           return Employees[employeeLevel].Count;
       }
    }
}
