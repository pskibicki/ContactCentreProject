using System.Threading.Tasks;
using AutoMapper;
using ContactCentre.Controllers;
using ContactCentre.Map;
using ContactCentre.Models;
using ContactCentre.Models.Dto;
using ContactCentre.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContactCentre.Tests.Controllers
{
    public class TestEmployeeController
    {
        [Fact]
        public void ControllerShouldReturnOkResponseWhenSucceededToCreateEmployee()
        {
            var employeeController = new EmployeeController(GetMapperMock(), GetEmployeeServiceMock(true).Object);

            var result = employeeController.CreateEmployeeAsync(new EmployeeRequest());

            Assert.Equal(StatusCodes.Status200OK, ((StatusCodeResult) result.Result).StatusCode);
        }

        [Fact]
        public void ControllerShouldReturnBadRequestResponseWhenFailedToCreateEmployee()
        {
            var employeeController = new EmployeeController(GetMapperMock(), GetEmployeeServiceMock(false).Object);

            var result = employeeController.CreateEmployeeAsync(new EmployeeRequest());

            Assert.Equal(StatusCodes.Status400BadRequest, ((StatusCodeResult) result.Result).StatusCode);
        }

        private IMapper GetMapperMock()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeMappingProfile());
            });
         
            return mockMapper.CreateMapper();
        }

        private Mock<IEmployeeService> GetEmployeeServiceMock(bool isSuccess)
        {
            var mock = new Mock<IEmployeeService>();
            mock.Setup(x => x.CreateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.FromResult(isSuccess));
            
            return mock;
        }
    }
}
