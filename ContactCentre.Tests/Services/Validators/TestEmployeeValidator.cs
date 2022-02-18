using System;
using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Service.Validators;
using Xunit;

namespace ContactCentre.Tests.Services.Validators
{
    public class TestEmployeeValidator
    {
        [Fact]
        public void ValidatorShouldReturnTrueForAgentEmployeeWhenThereIsNoOtherAgent()
        {
            var employee = GetFakeEmployee(LevelEnum.Agent);
            var employeeValidator = new EmployeeValidator();

            var isValid = employeeValidator.ValidateEmployee(employee, 0, 0);

            Assert.True(isValid);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForAgentEmployeeWhenThereIsOtherAgent()
        {
            var employee = GetFakeEmployee(LevelEnum.Agent);
            var employeeValidator = new EmployeeValidator();

            var isValid = employeeValidator.ValidateEmployee(employee, 1, 0);

            Assert.True(isValid);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForSupervisorEmployeeWhenThereIsNoOtherSupervisor()
        {
            var employee = GetFakeEmployee(LevelEnum.Supervisor);
            var employeeValidator = new EmployeeValidator();

            var result = employeeValidator.ValidateEmployee(employee, 0, 0);

            Assert.True(result);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForSupervisorEmployeeWhenThereIsOtherSupervisor()
        {
            var employee = GetFakeEmployee(LevelEnum.Supervisor);
            var employeeValidator = new EmployeeValidator();

            var result = employeeValidator.ValidateEmployee(employee, 1, 0);

            Assert.True(result);
        }

        [Fact]
        public void ValidatorShouldReturnTrueForGeneralManagerEmployeeWhenThereIsNoAnyOtherGeneralManager()
        {
            var employee = GetFakeEmployee(LevelEnum.GeneralManager);
            var employeeValidator = new EmployeeValidator();

            var result = employeeValidator.ValidateEmployee(employee, 0, 1);

            Assert.True(result);
        }

        [Fact]
        public void ValidatorShouldReturnFalseForGeneralManagerEmployeeWhenThereIOtherGeneralManager()
        {
            var employee = GetFakeEmployee(LevelEnum.GeneralManager);
            var employeeValidator = new EmployeeValidator();

            var result = employeeValidator.ValidateEmployee(employee, 1, 1);

            Assert.False(result);
        }

        private Employee GetFakeEmployee(LevelEnum level)
        {
            return new Employee()
            {
                Id = Guid.NewGuid(),
                Level = level
            };
        }
    }
}
