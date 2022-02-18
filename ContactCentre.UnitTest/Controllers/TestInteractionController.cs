using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContactCentre.Controllers;
using ContactCentre.Map;
using ContactCentre.Models;
using ContactCentre.Models.Dto;
using ContactCentre.Models.Enums;
using ContactCentre.Models.ResponseDto;
using ContactCentre.Service;
using ContactCentre.Service.HelperObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Controllers
{
    public class TestInteractionController
    {
        [Fact]
        public void ControllerShouldReturnOkResponseWhenRegisterEmptyListOfInteractions()
        {
            var interactionController = new InteractionController(GetContactCentreServiceMock().Object, GetMapperMock());

            var result = interactionController.RegisterInteraction(new List<InteractionRequest>());

            Assert.Equal(StatusCodes.Status200OK, ((StatusCodeResult)result.Result).StatusCode);
        }

        [Fact]
        public void ControllerShouldReturnBadRequestResponseWhenFailedToRegisterGivenInteractions()
        {
            var interactionController = new InteractionController(GetContactCentreServiceMock().Object, GetMapperMock());

            var result = interactionController.RegisterInteraction(GetValidRequestInteractions());

            Assert.Equal(StatusCodes.Status400BadRequest, ((StatusCodeResult)result.Result).StatusCode);
        }

        [Fact]
        public void ControllerShouldReturnOkResponseWhenSucceededToRegisterInteractions()
        {
            var requestInteractions = GetValidRequestInteractions();
            var interactions = GetValidInteractions();
            var interactionController = new InteractionController(GetContactCentreServiceMock(true, interactions).Object, GetMapperMock());

            var result = interactionController.RegisterInteraction(requestInteractions);

            var objectResult = (ObjectResult)result.Result;
            var responseInteractions = objectResult.Value as List<InteractionResponseDto>;
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(3, responseInteractions.Count);
        }

        private List<Interaction> GetValidInteractions()
        {
            return new List<Interaction>()
            {
                new Interaction() { InteractionType = InteractionTypeEnum.Voice, EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.Voice, EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.Voice, EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid() }
            };
        }

        private List<InteractionRequest> GetValidRequestInteractions()
        {
            return new List<InteractionRequest>()
            {
                new InteractionRequest() { InteractionType = InteractionTypeEnum.Voice},
                new InteractionRequest() { InteractionType = InteractionTypeEnum.NonVoice},
                new InteractionRequest() { InteractionType = InteractionTypeEnum.Voice}
            };
        }

        private Mock<IContactCentreService> GetContactCentreServiceMock(bool isSuccess = false, List<Interaction> interactions = null)
        {
            var registerResult = new RegisterResult() { Success = isSuccess, Interactions = interactions};

            var mock = new Mock<IContactCentreService>();
            mock.Setup(x => x.RegisterInteractionsAsync(It.IsAny<List<Interaction>>())).Returns(Task.FromResult(registerResult));

            return mock;
        }

        private IMapper GetMapperMock()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new InteractionMappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
