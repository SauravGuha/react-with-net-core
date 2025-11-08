

using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, ProfileViewModel>();
        }
    }
}