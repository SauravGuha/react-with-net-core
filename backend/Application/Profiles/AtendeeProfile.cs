

using Application.ViewModels;
using AutoMapper;


namespace Application.Profiles
{
    public class AtendeeProfile : Profile
    {
        public AtendeeProfile()
        {
            CreateMap<Domain.Models.Attendees, AttendeeViewModel>();
            CreateMap<AttendeeCommandViewModel, Domain.Models.Attendees>();
        }
    }
}