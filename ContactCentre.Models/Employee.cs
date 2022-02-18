using System;
using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public LevelEnum Level { get; set; }
    }
}
