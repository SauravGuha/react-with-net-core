

using System.Globalization;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            this.CreateMap<Activity, ActivityViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
            .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.EventDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture)));

            this.CreateMap<ActivityCommandViewModel, Activity>()
            .ForMember(d => d.EventDate,
            opt => opt.MapFrom(s => DateTime.ParseExact(s.EventDate.Trim('Z'), "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture)));
        }
    }
}