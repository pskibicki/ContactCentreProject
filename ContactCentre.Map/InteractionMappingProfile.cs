using System;
using AutoMapper;
using ContactCentre.Models;
using ContactCentre.Models.Dto;
using ContactCentre.Models.ResponseDto;

namespace ContactCentre.Map
{
    public class InteractionMappingProfile : Profile
    {
        public InteractionMappingProfile()
        {
            CreateMap<InteractionRequest, Interaction>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.NewGuid()));
            CreateMap<Interaction, InteractionRequest>();
            CreateMap<Interaction, InteractionResponseDto>();
        }
    }
}
