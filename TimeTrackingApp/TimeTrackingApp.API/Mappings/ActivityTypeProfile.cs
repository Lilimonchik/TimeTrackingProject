using AutoMapper;
using TimeTrackingApp.Domain.Model;
using TimeTrackingApp.Dto;

namespace TimeTrackingApp.API.Mappings
{
    public class ActivityTypeProfile: Profile
    {
        public ActivityTypeProfile()
        {
            CreateMap<ActivityType, ActivityTypeDto>();
            CreateMap<ActivityTypeDto, ActivityType>();
        }
    }
}
