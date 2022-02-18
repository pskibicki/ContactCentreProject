using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Repository
{
    public interface IAvailableEmployeeRepository
    {
        AvailableEmployee GetAvailableEmployee(LevelEnum level, AvailableInteractionTypeEnum availableInteractionType);
        Task DeleteAsync(AvailableEmployee availableEmployee);
        Task UpdateAsync(AvailableEmployee availableEmployee, AvailableInteractionTypeEnum availableInteractionTypeEnum);
        Task AddAsync(LevelEnum level, AvailableEmployee availableEmployee);
    }
}