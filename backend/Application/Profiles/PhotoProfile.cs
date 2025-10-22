

using Application.ViewModels;
using AutoMapper;

namespace Application.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Domain.Models.Photo, PhotoUploadResultViewModel>();
            CreateMap<Domain.Models.Photo, PhotoViewModel>();
        }
    }
}