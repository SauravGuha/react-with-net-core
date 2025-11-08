

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Photo.Query
{
    public class PhotoQueryRequest : IRequest<Result<IEnumerable<PhotoUploadResultViewModel>>>
    {
        public required Guid UserId { get; set; }
    }

    public class PhotoQueryRequestHandler : IRequestHandler<PhotoQueryRequest, Result<IEnumerable<PhotoUploadResultViewModel>>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IPhotoQueryRepository photoQueryRepository;
        private readonly IMapper mapper;

        public PhotoQueryRequestHandler(IUserAccessor userAccessor, IPhotoQueryRepository photoQueryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userAccessor = userAccessor;
            this.photoQueryRepository = photoQueryRepository;
        }
        public async Task<Result<IEnumerable<PhotoUploadResultViewModel>>> Handle(PhotoQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserByIdAsync(request.UserId.ToString());
            var userPhotos = await this.photoQueryRepository.GetAllAsync(e => e.UserId == user.Id, cancellationToken);
            var returnModel = mapper.Map<IEnumerable<PhotoUploadResultViewModel>>(userPhotos);
            return Result<IEnumerable<PhotoUploadResultViewModel>>.SetSuccess(returnModel!);
        }
    }
}