using System;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Repository;
using ContactCentre.Service.Validators;

namespace ContactCentre.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHierarchyService _hierarchyService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IAvailableEmployeeRepository _availableEmployeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidator employeeValidator, IAvailableEmployeeRepository availableEmployeeRepository, IHierarchyService hierarchyService)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _availableEmployeeRepository = availableEmployeeRepository;
            _hierarchyService = hierarchyService;
        }

        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            var employeeLimit = _hierarchyService.GetLimit(employee.Level);
            var employeeCount = await _employeeRepository.GetEmployeeCountAsync(employee.Level);

            if (!_employeeValidator.ValidateEmployee(employee, employeeLimit, employeeCount))
            {
                return false;
            }

            try
            {
                employee.Id = Guid.NewGuid();

                await _employeeRepository.CreateEmployeeAsync(employee);
                await _availableEmployeeRepository.AddAsync(employee.Level, new AvailableEmployee() { EmployeeId = employee.Id, AvailableInteractionType = AvailableInteractionTypeEnum.Voice });
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
