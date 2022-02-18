using System;
using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class Interaction
    {
        public Guid Id { get; set; }
        public InteractionTypeEnum InteractionType { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
