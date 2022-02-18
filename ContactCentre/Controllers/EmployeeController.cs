using System.Threading.Tasks;
using AutoMapper;
using ContactCentre.Models;
using ContactCentre.Models.Dto;
using ContactCentre.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactCentre.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateEmployeeAsync(EmployeeRequest employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var isSuccess = await _employeeService.CreateEmployeeAsync(employee);

            if (!isSuccess)
            {
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
