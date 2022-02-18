using System;
using System.Collections.Generic;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Service;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Services
{
    public class TestContactCentreService
    {
        [Fact]
        public async void ServiceShouldCallInteractionEmployeeAssignmentServiceForEachInteraction()
        {
            var interactions = GetInteractions();

            var interactionEmployeeAssignmentServiceMock = GetInteractionEmployeeAssignmentServiceMock();
            var contactCentreService = new ContactCentreService(interactionEmployeeAssignmentServiceMock.Object);

            await contactCentreService.RegisterInteractionsAsync(interactions);

            interactionEmployeeAssignmentServiceMock.Verify(x => x.AssignEmployeeAsync(It.IsAny<Interaction>()), Times.Exactly(interactions.Count));
        }

        [Fact]
        public async void ServiceShouldReturnOnlyAssignedInteractionsInRegisterResult()
        {
            var interactions = GetInteractions();

            var interactionEmployeeAssignmentServiceMock = GetInteractionEmployeeAssignmentServiceMock();
            var contactCentreService = new ContactCentreService(interactionEmployeeAssignmentServiceMock.Object);

            var registerResult = await contactCentreService.RegisterInteractionsAsync(interactions);

            Assert.True(registerResult.Success);
            Assert.Equal(4, registerResult.Interactions.Count);
        }


        private List<Interaction> GetInteractions()
        {
            return new List<Interaction>()
            {
                new Interaction() { InteractionType = InteractionTypeEnum.NonVoice, EmployeeId = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.Voice, EmployeeId = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.NonVoice, EmployeeId = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.Voice, EmployeeId = Guid.NewGuid() },
                new Interaction() { InteractionType = InteractionTypeEnum.Voice }
            };
        }

        private Mock<IInteractionEmployeeAssignmentService> GetInteractionEmployeeAssignmentServiceMock()
        {
            return new Mock<IInteractionEmployeeAssignmentService>();
        }
    }
}
