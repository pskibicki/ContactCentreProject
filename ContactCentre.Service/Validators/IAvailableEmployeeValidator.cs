using System.Collections.Generic;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Service.Validators
{
    public interface IAvailableEmployeeValidator
    {
        bool CanHandleInteraction(LevelEnum level, InteractionTypeEnum interactionType, List<InteractionEmployeeAssignment> interactionEmployeeAssignments);
    }
}