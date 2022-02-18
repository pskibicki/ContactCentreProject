using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactCentre.Models;

namespace ContactCentre.Repository
{
    public interface IInteractionEmployeeAssignmentRepository
    {
        Task<List<InteractionEmployeeAssignment>> GetInteractionsAssignmentsByEmployeeIdAsync(Guid id);
        Task<bool> AddAsync(InteractionEmployeeAssignment interactionEmployeeAssignment);
    }
}