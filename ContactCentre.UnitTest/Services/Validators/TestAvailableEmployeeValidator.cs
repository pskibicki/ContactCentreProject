using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Repository;
using ContactCentre.Service.Validators;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Services.Validators
{
    public class TestAvailableEmployeeValidator
    {
        [Fact]
        public void ValidatorShouldReturnTrueForVoiceInteractionWhenAvailableEmployeeIsAgentAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Agent, InteractionTypeEnum.Voice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForNonVoiceInteractionWhenAvailableEmployeeIsAgentAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Agent, InteractionTypeEnum.NonVoice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForNonVoiceInteractionWhenAvailableEmployeeIsAgentAndHasAssignedNonVoiceInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();
            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.NonVoice } };
            
            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Agent, InteractionTypeEnum.NonVoice, interactionEmployeeAssignments);

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnFalseForVoiceInteractionWhenAvailableEmployeeIsAgentAndHasAssignedVoiceInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();
            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.Voice } };
            
            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Agent, InteractionTypeEnum.Voice, interactionEmployeeAssignments);

            Assert.False(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForVoiceInteractionWhenAvailableEmployeeIsSupervisorAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Supervisor, InteractionTypeEnum.Voice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForNonVoiceInteractionWhenAvailableEmployeeIsSupervisorAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Supervisor, InteractionTypeEnum.NonVoice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForNonVoiceInteractionWhenAvailableEmployeeIsSupervisorAndHasAssignedNonVoiceInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();
            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.NonVoice } };

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Supervisor, InteractionTypeEnum.NonVoice, interactionEmployeeAssignments);

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnFalseForVoiceInteractionWhenAvailableEmployeeIsSupervisorAndHasAssignedVoiceInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();
            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.Voice } };
            
            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.Supervisor, InteractionTypeEnum.Voice, interactionEmployeeAssignments);

            Assert.False(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForVoiceInteractionWhenAvailableEmployeeIsGeneralManagerAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.GeneralManager, InteractionTypeEnum.Voice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForNonVoiceInteractionWhenAvailableEmployeeIsGeneralManagerAndHasNoAssignedInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.GeneralManager, InteractionTypeEnum.NonVoice, new List<InteractionEmployeeAssignment>());

            Assert.True(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnFalseForNonVoiceInteractionWhenAvailableEmployeeIsGeneralManagerAndHasAssignedNonVoiceInteraction()
        {

            var availableEmployeeValidator = new AvailableEmployeeValidator();
            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.NonVoice } };
            
            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.GeneralManager, InteractionTypeEnum.NonVoice, interactionEmployeeAssignments);

            Assert.False(canHandleInteraction);
        }

        [Fact]
        public void ValidatorShouldReturnFalseForVoiceInteractionWhenAvailableEmployeeIsGeneralManagerAndHasAssignedVoiceInteraction()
        {
            var availableEmployeeValidator = new AvailableEmployeeValidator();

            var interactionEmployeeAssignments = new List<InteractionEmployeeAssignment>() { new InteractionEmployeeAssignment() { InteractionType = InteractionTypeEnum.Voice } };
            var canHandleInteraction = availableEmployeeValidator.CanHandleInteraction(LevelEnum.GeneralManager, InteractionTypeEnum.Voice, interactionEmployeeAssignments);

            Assert.False(canHandleInteraction);
        }
    }
}
