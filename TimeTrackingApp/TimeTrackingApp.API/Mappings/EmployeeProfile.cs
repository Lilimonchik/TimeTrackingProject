using AutoMapper;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.API.Mappings
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
