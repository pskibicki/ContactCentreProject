using System.Threading.Tasks;
using ContactCentre.Models;

namespace ContactCentre.Service
{
    public interface IInteractionEmployeeAssignmentService
    {
        Task<bool?> AssignEmployeeAsync(Interaction interaction);
    }
}