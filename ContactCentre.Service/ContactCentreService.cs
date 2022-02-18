using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Service.HelperObjects;

namespace ContactCentre.Service
{
    public class ContactCentreService : IContactCentreService
    {
        private readonly IInteractionEmployeeAssignmentService _interactionEmployeeAssignmentService;

        public ContactCentreService(IInteractionEmployeeAssignmentService interactionEmployeeAssignmentService)
        {
            _interactionEmployeeAssignmentService = interactionEmployeeAssignmentService;
        }

        public async Task<RegisterResult> RegisterInteractionsAsync(List<Interaction> interactions)
        {
            foreach (var interaction in interactions)
            {
                await AssignFreeEmployeeAsync(interaction);
            }

            return RegisterResult.Ok(interactions.Where(x => !Guid.Empty.Equals(x.EmployeeId)).ToList());
        }

        private async Task AssignFreeEmployeeAsync(Interaction interaction)
        {
            await _interactionEmployeeAssignmentService.AssignEmployeeAsync(interaction);
        }
    }
}
