using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactCentre.Models;

namespace ContactCentre.Repository
{
    public class InteractionEmployeeAssignmentRepository : IInteractionEmployeeAssignmentRepository
    {
        private static Dictionary<Guid, List<InteractionEmployeeAssignment>> InteractionsEmployeeAssignments = new Dictionary<Guid, List<InteractionEmployeeAssignment>>(); 

        public async Task<List<InteractionEmployeeAssignment>> GetInteractionsAssignmentsByEmployeeIdAsync(Guid id)
        {
            if (!InteractionsEmployeeAssignments.ContainsKey(id))
            {
                return null;
            }

            return InteractionsEmployeeAssignments[id];
        }

        public async Task<bool> AddAsync(InteractionEmployeeAssignment interactionEmployeeAssignment)
        {
            if (!InteractionsEmployeeAssignments.ContainsKey(interactionEmployeeAssignment.EmployeeId))
            {
                InteractionsEmployeeAssignments.Add(interactionEmployeeAssignment.EmployeeId, new List<InteractionEmployeeAssignment>());
            }

            InteractionsEmployeeAssignments[interactionEmployeeAssignment.EmployeeId].Add(interactionEmployeeAssignment);

            return true;
        }
    }
}
