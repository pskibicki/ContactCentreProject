using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Repository;
using ContactCentre.Service.Validators;

namespace ContactCentre.Service
{
    public class InteractionEmployeeAssignmentService : IInteractionEmployeeAssignmentService
    {
        private readonly IInteractionEmployeeAssignmentRepository _interactionEmployeeAssignmentRepository;
        private readonly IAvailableEmployeeRepository _availableEmployeeRepository;
        private readonly IHierarchyService _hierarchyService;
        private readonly IAvailableEmployeeValidator _availableEmployeeValidator;
        private readonly IEmployeeRepository _employeeRepository;

        public InteractionEmployeeAssignmentService(
            IInteractionEmployeeAssignmentRepository interactionEmployeeAssignmentRepository,
            IHierarchyService hierarchyService, 
            IAvailableEmployeeRepository availableEmployeeRepository, 
            IAvailableEmployeeValidator availableEmployeeValidator, 
            IEmployeeRepository employeeRepository)
        {
            _hierarchyService = hierarchyService;
            _interactionEmployeeAssignmentRepository = interactionEmployeeAssignmentRepository;
            _availableEmployeeRepository = availableEmployeeRepository;
            _availableEmployeeValidator = availableEmployeeValidator;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool?> AssignEmployeeAsync(Interaction interaction)
        {
            var availableEmployee = await GetAvailableEmployeeAsync(interaction);

            if (availableEmployee == null)
            {
                if (!SetHigherLevel())
                {
                    ResetLevel();
                    return null;
                }

                return await AssignEmployeeAsync(interaction);
            }

            var employee = await _employeeRepository.GetEmployeeAsync(availableEmployee.EmployeeId);
            var interactionEmployeeAssignments = await _interactionEmployeeAssignmentRepository.GetInteractionsAssignmentsByEmployeeIdAsync(availableEmployee.EmployeeId);

            if (_availableEmployeeValidator.CanHandleInteraction(employee.Level, interaction.InteractionType, interactionEmployeeAssignments))
            {
                interaction.EmployeeId = availableEmployee.EmployeeId;
                await SaveAsync(interaction, availableEmployee);
                ResetLevel();
            }
            else
            {
                return await AssignEmployeeAsync(interaction);
            }

            return true;
        }

        private async Task SaveAsync(Interaction interaction, AvailableEmployee availableEmployee)
        {
            await _interactionEmployeeAssignmentRepository.AddAsync(new InteractionEmployeeAssignment()
            {
                EmployeeId = availableEmployee.EmployeeId,
                InteractionId = interaction.Id,
                InteractionType = interaction.InteractionType
            });

            if (availableEmployee.AvailableInteractionType >= (AvailableInteractionTypeEnum)interaction.InteractionType)
            {
                if (availableEmployee.AvailableInteractionType == (AvailableInteractionTypeEnum)interaction.InteractionType)
                {
                    await _availableEmployeeRepository.DeleteAsync(availableEmployee);
                }
                else
                {
                    await _availableEmployeeRepository.UpdateAsync(availableEmployee, (AvailableInteractionTypeEnum)interaction.InteractionType);
                }
            }
        }

        private async Task<AvailableEmployee> GetAvailableEmployeeAsync(Interaction interaction)
        {
            return _availableEmployeeRepository.GetAvailableEmployee(_hierarchyService.CurrentLevel.Level, (AvailableInteractionTypeEnum) interaction.InteractionType);
        }

        private bool SetHigherLevel()
        {
            return _hierarchyService.MoveLevelUp();
        }

        private void ResetLevel()
        {
            _hierarchyService.ResetLevel();
        }
    }
}
