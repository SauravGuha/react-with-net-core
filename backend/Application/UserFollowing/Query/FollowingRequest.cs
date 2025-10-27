

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.UserRepository;
using MediatR;

namespace Application.UserFollowing.Query
{
    public class FollowingRequest : IRequest<Result<List<UserViewModel>>>
    {
        public required string UserId { get; set; }
    }

    public class FollowingsRequestHandler : IRequestHandler<FollowingRequest, Result<List<UserViewModel>>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IUserFollowingQueryRepository userFollowingQueryRepository;
        private readonly IMapper mapper;

        public FollowingsRequestHandler(IUserAccessor userAccessor,
        IUserFollowingQueryRepository userFollowingQueryRepository,
        IMapper mapper)
        {
            this.mapper = mapper;
            this.userFollowingQueryRepository = userFollowingQueryRepository;
            this.userAccessor = userAccessor;
        }
        public async Task<Result<List<UserViewModel>>> Handle(FollowingRequest request, CancellationToken cancellationToken)
        {
            var userFollowings = await this.userFollowingQueryRepository
            .GetAllAsync(e => e.ObserverId == request.UserId, cancellationToken, nameof(Domain.Models.UserFollowing.Target));
            return Result<List<UserViewModel>>.SetSuccess(mapper.Map<List<UserViewModel>>(userFollowings.Select(e => e.Target))!);
        }
    }
}