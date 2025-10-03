

using Application.Core;
using Application.ViewModels;
using Domain.Infrastructure;
using MediatR;

namespace Application.Photo.Command
{
    public class PhotoCommandRequest : IRequest<Result<PhotoUploadResultViewModel>>
    {
        public required Stream PhotoStream { get; set; }
    }

    public class PhotoCommandRequestHandler : IRequestHandler<PhotoCommandRequest, Result<PhotoUploadResultViewModel>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IImageRepository imageRepository;

        public PhotoCommandRequestHandler(IUserAccessor userAccessor, IImageRepository imageRepository)
        {
            this.userAccessor = userAccessor;
            this.imageRepository = imageRepository;
        }

        public async Task<Result<PhotoUploadResultViewModel>> Handle(PhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetUserId();
            var result = await imageRepository.StoreImageAsync(userId, request.PhotoStream, cancellationToken);
            return Result<PhotoUploadResultViewModel>.SetSuccess(new PhotoUploadResultViewModel { PublicId = result.photoId, Url = result.url });
        }
    }
}