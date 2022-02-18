using System.Threading.Tasks;
using ContactCentre.Models;

namespace ContactCentre.Service
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployeeAsync(Employee employee);
    }
}