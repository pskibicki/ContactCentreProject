using System;
using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class InteractionEmployeeAssignment
    {
        public Guid EmployeeId { get; set; }
        public Guid InteractionId { get; set; }
        public InteractionTypeEnum InteractionType { get; set; }
    }
}
