using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Repository
{
    public class AvailableEmployeeRepository : IAvailableEmployeeRepository
    {
        private static readonly Dictionary<LevelEnum, List<AvailableEmployee>> AvailableEmployees = new Dictionary<LevelEnum, List<AvailableEmployee>>();

        public AvailableEmployee GetAvailableEmployee(LevelEnum level, AvailableInteractionTypeEnum availableInteractionType)
        {
            if (!AvailableEmployees.ContainsKey(level))
            {
                return null;
            }

            return AvailableEmployees[level].FirstOrDefault(x => x.AvailableInteractionType >= availableInteractionType);
        }

        public async Task AddAsync(LevelEnum level, AvailableEmployee availableEmployee)
        {
            if (!AvailableEmployees.ContainsKey(level))
            {
                AvailableEmployees.Add(level, new List<AvailableEmployee>());
            }

            AvailableEmployees[level].Add(availableEmployee);
        }

        public async Task DeleteAsync(AvailableEmployee availableEmployee)
        {
            var key = AvailableEmployees.FirstOrDefault(x => x.Value.Contains(availableEmployee)).Key;
            AvailableEmployees[key].Remove(availableEmployee);
        }

        public async Task UpdateAsync(AvailableEmployee availableEmployee, AvailableInteractionTypeEnum availableInteractionType)
        {
            var employee = AvailableEmployees.SelectMany(x => x.Value).FirstOrDefault(x => x.EmployeeId == availableEmployee.EmployeeId);
            var updatedAvailableInteractionType = employee.AvailableInteractionType - (availableInteractionType + 1);

            employee.AvailableInteractionType = (AvailableInteractionTypeEnum)updatedAvailableInteractionType;
        }
    }
}
