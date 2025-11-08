

using System.Globalization;
using Application.ViewModels;
using AutoMapper;

namespace Application.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCommandViewModel, Domain.Models.Comment>();
            CreateMap<Domain.Models.Comment, CommentViewModel>()
            .ForMember(cvm => cvm.DisplayName, mo => mo.MapFrom(c => c.User.DisplayName))
            .ForMember(cvm => cvm.ImageUrl, mo => mo.MapFrom(c => c.User.ImageUrl))
            .ForMember(cvm => cvm.CreatedAt,
            mo => mo.MapFrom(c => c.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture)));
        }
    }
}