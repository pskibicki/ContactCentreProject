using System;
using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class AvailableEmployee
    {
        public Guid EmployeeId { get; set; }
        public AvailableInteractionTypeEnum AvailableInteractionType { get; set; }
    }
}
