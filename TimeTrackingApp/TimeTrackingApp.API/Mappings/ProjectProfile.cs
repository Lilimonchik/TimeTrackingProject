using AutoMapper;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.API.Mappings
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile() {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
