
using System.Net;
using Application.Core;
using Domain.Infrastructure;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Photo.Command
{
    public class PhotoDeleteRequest : IRequest<Result<Unit>>
    {
        public required Guid ImageId { get; set; }
    }

    public class PhotoDeleteRequestHandler : IRequestHandler<PhotoDeleteRequest, Result<Unit>>
    {
        private readonly IPhotoQueryRepository photoQueryRepository;
        private readonly IPhotoCommandRepository photoCommandRepository;
        private readonly IUserAccessor userAccessor;
        private readonly IImageRepository imageRepository;

        public PhotoDeleteRequestHandler(IPhotoQueryRepository photoQueryRepository,
        IPhotoCommandRepository photoCommandRepository, IUserAccessor userAccessor, IImageRepository imageRepository)
        {
            this.photoCommandRepository = photoCommandRepository;
            this.userAccessor = userAccessor;
            this.imageRepository = imageRepository;
            this.photoQueryRepository = photoQueryRepository;
        }
        public async Task<Result<Unit>> Handle(PhotoDeleteRequest request, CancellationToken cancellationToken)
        {
            var activity = await photoQueryRepository.GetById(request.ImageId, cancellationToken);
            if (activity != null)
            {
                var userId = userAccessor.GetUserId();
                await photoCommandRepository.DeleteAsync(activity, cancellationToken);
                await imageRepository.DeleteImage(userId, request.ImageId.ToString(), "jpg", cancellationToken);
            }
            else
                return Result<Unit>.SetError($"{request.ImageId} not found", (int)HttpStatusCode.NotFound);

            return Result<Unit>.SetSuccess(Unit.Value);
        }
    }
}