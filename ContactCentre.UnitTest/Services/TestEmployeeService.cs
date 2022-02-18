using ContactCentre.Models;
using ContactCentre.Models.Enums;
using ContactCentre.Repository;
using ContactCentre.Service;
using ContactCentre.Service.Validators;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Services
{
    public class TestEmployeeService
    {
        [Fact]
        public void ServiceShouldReturnFalseGivenEmployeeIsNotValid()
        {
            var employee = GetValidEmployee();

            var employeeService = new EmployeeService(GetEmployeeRepositoryMock().Object, GetEmployeeValidatorMock(false).Object, GetAvailableEmployeeRepositoryMock().Object, new HierarchyService());

            var result = employeeService.CreateEmployeeAsync(employee);

            Assert.False(result.Result);
        }

        [Fact]
        public void ServiceShouldReturnTrueWhenGivenEmployeeIsValid()
        {
            var employee = GetValidEmployee();

            var employeeRepositoryMock = GetEmployeeRepositoryMock();
            var availableEmployeeRepositoryMock = GetAvailableEmployeeRepositoryMock();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object, GetEmployeeValidatorMock(true).Object, availableEmployeeRepositoryMock.Object, new HierarchyService());

            var result = employeeService.CreateEmployeeAsync(employee);

            Assert.True(result.Result);
        }

        [Fact]
        public async void ServiceShouldCallEmployeeRepositoryAndAvailableEmployeeRepositoryToSaveData()
        {
            var employee = GetValidEmployee();

            var employeeRepositoryMock = GetEmployeeRepositoryMock();
            var availableEmployeeRepositoryMock = GetAvailableEmployeeRepositoryMock();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object, GetEmployeeValidatorMock(true).Object, availableEmployeeRepositoryMock.Object, new HierarchyService());

            await employeeService.CreateEmployeeAsync(employee);

            employeeRepositoryMock.Verify(x => x.CreateEmployeeAsync(employee));
            availableEmployeeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<LevelEnum>(), It.IsAny<AvailableEmployee>()));
        }

        [Fact]
        public async void ServiceShouldCallHierarchyServiceToGetEmployeeLimitValue()
        {
            var employee = GetValidEmployee();
            
            var hierarchyServiceMock = new Mock<IHierarchyService>();
            var employeeService = new EmployeeService(GetEmployeeRepositoryMock().Object, GetEmployeeValidatorMock(true).Object, GetAvailableEmployeeRepositoryMock().Object, hierarchyServiceMock.Object);

            await employeeService.CreateEmployeeAsync(employee);

            hierarchyServiceMock.Verify(x => x.GetLimit(It.IsAny<LevelEnum>()));
        }

        [Fact]
        public async void ServiceShouldCallEmployeeRepositoryToGetEmployeeCountValue()
        {
            var employee = GetValidEmployee();

            var employeeRepositoryMock = GetEmployeeRepositoryMock();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object, GetEmployeeValidatorMock(true).Object, GetAvailableEmployeeRepositoryMock().Object, new HierarchyService());

            await employeeService.CreateEmployeeAsync(employee);

            employeeRepositoryMock.Verify(x => x.GetEmployeeCountAsync(It.IsAny<LevelEnum>()));
        }

        private Employee GetValidEmployee()
        {
            return new Employee() { Level = LevelEnum.Agent };
        }

        private Mock<IAvailableEmployeeRepository> GetAvailableEmployeeRepositoryMock()
        {
            return new Mock<IAvailableEmployeeRepository>();
        }

        private Mock<IEmployeeValidator> GetEmployeeValidatorMock(bool isValid)
        {
            var employeeValidatorMock = new Mock<IEmployeeValidator>();
            employeeValidatorMock.Setup(x => x.ValidateEmployee(It.IsAny<Employee>(), It.IsAny<int>(), It.IsAny<int>())).Returns((isValid));

            return employeeValidatorMock;
        }

        private Mock<IEmployeeRepository> GetEmployeeRepositoryMock()
        {
            return new Mock<IEmployeeRepository>();
        }
    }
}
