using AutoMapper;
using ContactCentre.Models;
using ContactCentre.Models.Dto;

namespace ContactCentre.Map
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeRequest>();
            CreateMap<EmployeeRequest, Employee>();
        }
    }
}
