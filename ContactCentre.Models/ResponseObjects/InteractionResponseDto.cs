using System;
using ContactCentre.Models.Dto;

namespace ContactCentre.Models.ResponseDto
{
    public class InteractionResponseDto : InteractionRequest
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
