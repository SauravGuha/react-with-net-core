

using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class LocationIqProfile : Profile
    {
        public LocationIqProfile()
        {
            this.CreateMap<LocationIQ, LocationIqViewModel>();
        }
    }
}