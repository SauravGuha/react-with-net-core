

using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCommandViewModel, Domain.Models.Comment>();
            CreateMap<Domain.Models.Comment, CommentViewModel>()
            .ForMember(cvm => cvm.DisplayName, mo => mo.MapFrom(c => c.User.DisplayName))
            .ForMember(cvm => cvm.ImageUrl, mo => mo.MapFrom(c => c.User.ImageUrl));
        }
    }
}