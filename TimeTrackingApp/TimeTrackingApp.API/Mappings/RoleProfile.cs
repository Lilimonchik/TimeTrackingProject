using AutoMapper;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.API.Mappings
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    }
}
