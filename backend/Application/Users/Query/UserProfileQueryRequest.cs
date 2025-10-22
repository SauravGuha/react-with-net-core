
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Users.Query
{
    public class UserProfileQueryRequest : IRequest<Result<ProfileViewModel>>
    {
        public Guid Id { get; set; }
    }

    public class UserProfileQueryHandler : IRequestHandler<UserProfileQueryRequest, Result<ProfileViewModel>>
    {
        private readonly IPhotoQueryRepository photoQueryRepository;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public UserProfileQueryHandler(IPhotoQueryRepository photoQueryRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            this.photoQueryRepository = photoQueryRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<Result<ProfileViewModel>> Handle(UserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userAccessor.GetUserByIdAsync(request.Id.ToString());
            var photoDetails = await this.photoQueryRepository
            .GetAllAsync(e => e.UserId == request.Id.ToString(), cancellationToken, $"{nameof(User)}");

            if (user == null)
                return Result<ProfileViewModel>.SetError("User not found", 404);
            else
            {
                var result = mapper.Map<ProfileViewModel>(user);
                if (photoDetails != null && photoDetails.Any())
                    result!.Photos = mapper.Map<List<PhotoViewModel>>(photoDetails)!;
                return Result<ProfileViewModel>.SetSuccess(result!);
            }
        }
    }
}