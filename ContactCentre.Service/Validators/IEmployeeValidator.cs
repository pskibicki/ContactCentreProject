using ContactCentre.Models;

namespace ContactCentre.Service.Validators
{
    public interface IEmployeeValidator
    {
        bool ValidateEmployee(Employee employee, int limit, int employeeCount);
    }
}