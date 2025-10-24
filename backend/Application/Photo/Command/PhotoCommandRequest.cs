

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Photo.Command
{
    public class PhotoCommandRequest : IRequest<Result<List<PhotoUploadResultViewModel>>>
    {
        public required Stream PhotoStream { get; set; }
    }

    public class PhotoCommandRequestHandler : IRequestHandler<PhotoCommandRequest, Result<List<PhotoUploadResultViewModel>>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IImageRepository imageRepository;
        private readonly IPhotoCommandRepository photoCommandRepository;
        private readonly IMapper mapper;

        public PhotoCommandRequestHandler(IUserAccessor userAccessor, IImageRepository imageRepository,
         IPhotoCommandRepository photoCommandRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.photoCommandRepository = photoCommandRepository;
            this.userAccessor = userAccessor;
            this.imageRepository = imageRepository;
        }

        public async Task<Result<List<PhotoUploadResultViewModel>>> Handle(PhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var userInfo = await userAccessor.GetUserAsync();
            var result = await imageRepository.StoreImageAsync(userInfo.Id, "jpg", request.PhotoStream, cancellationToken);
            var photoResult = await photoCommandRepository.CreateAsync(new Domain.Models.Photo
            {
                Id = Guid.Parse(result.photoId),
                PublicId = result.photoId,
                Url = result.url,
                UserId = userInfo.Id,
                CreatedBy = userInfo.Email
            }, cancellationToken);
            return Result<List<PhotoUploadResultViewModel>>
            .SetSuccess(mapper.Map<List<PhotoUploadResultViewModel>>(new List<Domain.Models.Photo> { photoResult })!);
        }
    }
}