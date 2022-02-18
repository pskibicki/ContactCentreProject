using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Repository;
using ContactCentre.Service;
using ContactCentre.Service.Validators;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Services
{
    public class TestInteractionEmployeeAssignmentService
    {

        [Fact]
        public async void ServiceShouldSaveInteractionEmployeeAssignmentAndUpdateAvailableEmployeeWhenItIsStillAbleToHandleSomeInteraction()
        {
            AvailableEmployee availableEmployee = GetValidAvailableEmployee(AvailableInteractionTypeEnum.Voice);

            var interactionEmployeeAssignmentRepositoryMock = GetInteractionEmployeeAssignmentRepositoryMock();
            var availableEmployeeRepositoryMock = GetAvailableEmployeeRepositoryMock(availableEmployee);
            var interactionEmployeeAssignmentService = new InteractionEmployeeAssignmentService(interactionEmployeeAssignmentRepositoryMock.Object,
                new HierarchyService(),
                availableEmployeeRepositoryMock.Object,
                GetAvailableEmployeeValidator(true).Object,
                GetEmployeeRepositoryMock().Object);

            await interactionEmployeeAssignmentService.AssignEmployeeAsync(new Interaction() { InteractionType = InteractionTypeEnum.NonVoice});

            interactionEmployeeAssignmentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<InteractionEmployeeAssignment>()));
            availableEmployeeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<AvailableEmployee>(), It.IsAny<AvailableInteractionTypeEnum>()));
        }

        [Fact]
        public async void ServiceShouldSaveInteractionEmployeeAssignmentAndDeleteAvailableEmployeeWhenItIsNoLongerAbleToHandleSomeInteraction()
        {
            AvailableEmployee availableEmployee = GetValidAvailableEmployee(AvailableInteractionTypeEnum.NonVoice);

            var interactionEmployeeAssignmentRepositoryMock = GetInteractionEmployeeAssignmentRepositoryMock();
            var availableEmployeeRepositoryMock = GetAvailableEmployeeRepositoryMock(availableEmployee);
            var interactionEmployeeAssignmentService = new InteractionEmployeeAssignmentService(interactionEmployeeAssignmentRepositoryMock.Object,
                new HierarchyService(),
                availableEmployeeRepositoryMock.Object,
                GetAvailableEmployeeValidator(true).Object,
                GetEmployeeRepositoryMock().Object);

            await interactionEmployeeAssignmentService.AssignEmployeeAsync(new Interaction() { InteractionType = InteractionTypeEnum.NonVoice });

            interactionEmployeeAssignmentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<InteractionEmployeeAssignment>()));
            availableEmployeeRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<AvailableEmployee>()));
        }

        [Fact]
        public async void ServiceShouldReturnNullWhenThereIsNoAvailableEmployeeToHandleInteraction()
        {
            var availableEmployeeRepositoryMock = GetAvailableEmployeeRepositoryMock(null);
            var interactionEmployeeAssignmentService = new InteractionEmployeeAssignmentService(GetInteractionEmployeeAssignmentRepositoryMock().Object,
                new HierarchyService(),
                availableEmployeeRepositoryMock.Object,
                GetAvailableEmployeeValidator(true).Object,
                GetEmployeeRepositoryMock().Object);

            var result = await interactionEmployeeAssignmentService.AssignEmployeeAsync(new Interaction() { InteractionType = InteractionTypeEnum.NonVoice });

            Assert.Null(result);
            availableEmployeeRepositoryMock.Verify(x => x.GetAvailableEmployee(It.IsAny<LevelEnum>(), It.IsAny<AvailableInteractionTypeEnum>()), Times.Exactly(3));
        }

        [Fact]
        public async void ServiceShouldCallEmployeeRepositoryToGetEmployee()
        {
            AvailableEmployee availableEmployee = GetValidAvailableEmployee(AvailableInteractionTypeEnum.Voice);

            var employeeRepositoryMock = GetEmployeeRepositoryMock();
            var interactionEmployeeAssignmentService = new InteractionEmployeeAssignmentService(GetInteractionEmployeeAssignmentRepositoryMock().Object,
                new HierarchyService(),
                GetAvailableEmployeeRepositoryMock(availableEmployee).Object,
                GetAvailableEmployeeValidator(true).Object,
                employeeRepositoryMock.Object);

            await interactionEmployeeAssignmentService.AssignEmployeeAsync(new Interaction() { InteractionType = InteractionTypeEnum.NonVoice });

            employeeRepositoryMock.Verify(x => x.GetEmployeeAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public async void ServiceShouldCallInteractionEmployeeAssignmentRepositoryToGetEmployeeInteractionAssignments()
        {
            AvailableEmployee availableEmployee = GetValidAvailableEmployee(AvailableInteractionTypeEnum.Voice);

            var interactionEmployeeAssignmentRepositoryMock = GetInteractionEmployeeAssignmentRepositoryMock();
            var interactionEmployeeAssignmentService = new InteractionEmployeeAssignmentService(interactionEmployeeAssignmentRepositoryMock.Object,
                new HierarchyService(),
                GetAvailableEmployeeRepositoryMock(availableEmployee).Object,
                GetAvailableEmployeeValidator(true).Object,
                GetEmployeeRepositoryMock().Object);

            await interactionEmployeeAssignmentService.AssignEmployeeAsync(new Interaction() { InteractionType = InteractionTypeEnum.NonVoice });

            interactionEmployeeAssignmentRepositoryMock.Verify(x => x.GetInteractionsAssignmentsByEmployeeIdAsync(It.IsAny<Guid>()));
        }

        private AvailableEmployee GetValidAvailableEmployee(AvailableInteractionTypeEnum availableInteractionType)
        {
            return new AvailableEmployee() { AvailableInteractionType = availableInteractionType };
        }

        private Mock<IAvailableEmployeeValidator> GetAvailableEmployeeValidator(bool canHandleInteraction)
        {
            var mock = new Mock<IAvailableEmployeeValidator>();
            mock.Setup(x => x.CanHandleInteraction(It.IsAny<LevelEnum>(), It.IsAny<InteractionTypeEnum>(), It.IsAny<List<InteractionEmployeeAssignment>>()))
                .Returns(canHandleInteraction);

            return mock;
        }

        private Mock<IAvailableEmployeeRepository> GetAvailableEmployeeRepositoryMock(AvailableEmployee availableEmployee)
        {
            var mock = new Mock<IAvailableEmployeeRepository>();
            mock.Setup(x => x.GetAvailableEmployee(It.IsAny<LevelEnum>(), It.IsAny<AvailableInteractionTypeEnum>()))
                .Returns(availableEmployee);

            return mock;
        }

        private Mock<IInteractionEmployeeAssignmentRepository> GetInteractionEmployeeAssignmentRepositoryMock()
        {
            return new Mock<IInteractionEmployeeAssignmentRepository>();
        }

        private Mock<IEmployeeRepository> GetEmployeeRepositoryMock()
        {
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.GetEmployeeAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Employee()));

            return employeeRepositoryMock;
        }
    }
}
