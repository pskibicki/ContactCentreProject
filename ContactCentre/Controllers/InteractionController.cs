using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactCentre.Models;
using ContactCentre.Models.Dto;
using ContactCentre.Models.ResponseDto;
using ContactCentre.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactCentre.Controllers
{
    [ApiController]
    [Route("interaction")]
    public class InteractionController : ControllerBase
    {
        private readonly IContactCentreService _contactCentreService;
        private readonly IMapper _mapper;

        public InteractionController(IContactCentreService contactCentreService, IMapper mapper)
        {
            _contactCentreService = contactCentreService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterInteraction(List<InteractionRequest> interactionDtoList)
        {
            if (!interactionDtoList.Any())
            {
                return Ok();
            }

            var interactions = _mapper.Map<List<Interaction>>(interactionDtoList);

            var result = await _contactCentreService.RegisterInteractionsAsync(interactions);

            if (!result.Success)
            {
                return new BadRequestResult();
            }

            var interactionResponseDtoList = _mapper.Map<List<InteractionResponseDto>>(result.Interactions);

            return new OkObjectResult(interactionResponseDtoList);
        }
    }
}
