using AutoMapper;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.API.Mappings
{
    public class ActivityProfile: Profile
    {
        public ActivityProfile() 
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityDto, Activity>();
        }
    }
}
