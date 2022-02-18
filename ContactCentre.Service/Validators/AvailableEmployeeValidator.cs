using System;
using System.Collections.Generic;
using System.Linq;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Service.Validators
{
    public class AvailableEmployeeValidator : IAvailableEmployeeValidator
    {
        private readonly Dictionary<LevelEnum, Func<InteractionTypeEnum, int>> maxInteractionsMap =
            new Dictionary<LevelEnum, Func<InteractionTypeEnum, int>>()
            {
                {
                    LevelEnum.Agent,
                    interactionType => interactionType switch
                    {
                        InteractionTypeEnum.Voice => 1,
                        InteractionTypeEnum.NonVoice => 2,
                        _ => throw new ArgumentOutOfRangeException(nameof(interactionType), interactionType, null)
                    }
                },
                {
                    LevelEnum.Supervisor,
                    interactionType => interactionType switch
                    {
                        InteractionTypeEnum.Voice => 1,
                        InteractionTypeEnum.NonVoice => 2,
                        _ => throw new ArgumentOutOfRangeException(nameof(interactionType), interactionType, null)
                    }
                },
                {
                    LevelEnum.GeneralManager, interactionType => 1
                }
            };

        public bool CanHandleInteraction(LevelEnum level, InteractionTypeEnum interactionType, List<InteractionEmployeeAssignment> interactionEmployeeAssignments)
        {
            if (interactionEmployeeAssignments == null)
            {
                return true;
            }
            
            return interactionEmployeeAssignments.Count(x => x.InteractionType == interactionType) < maxInteractionsMap[level](interactionType);
        }
    }
}
