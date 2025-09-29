

using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class AtendeeProfile : Profile
    {
        public AtendeeProfile()
        {
            CreateMap<Attendees, AttendeeViewModel>();
        }
    }
}