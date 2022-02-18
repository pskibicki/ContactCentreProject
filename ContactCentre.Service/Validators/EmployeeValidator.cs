using System;
using ContactCentre.Models;

namespace ContactCentre.Service.Validators
{
    public class EmployeeValidator : IEmployeeValidator
    {
        public bool ValidateEmployee(Employee employee, int limit, int employeeCount)
        {
            return ValidateEmployeeLevel(employee) && ValidateEmployeeLimit(limit, employeeCount);
        }

        private bool ValidateEmployeeLevel(Employee employee)
        {
            return Enum.IsDefined(employee.Level);
        }

        private bool ValidateEmployeeLimit( int limit, int employeeCount)
        {
            if (limit == 0)
            {
                return true;
            }

            return employeeCount < limit;
        }
    }
}
