

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.UserRepository;
using MediatR;

namespace Application.UserFollowing.Query
{
    public class FollowersRequest : IRequest<Result<List<UserViewModel>>>
    {
        public required string UserId { get; set; }
    }

    public class FollowersRequestHandler : IRequestHandler<FollowersRequest, Result<List<UserViewModel>>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IUserFollowingQueryRepository userFollowingQueryRepository;
        private readonly IMapper mapper;

        public FollowersRequestHandler(IUserAccessor userAccessor,
        IUserFollowingQueryRepository userFollowingQueryRepository,
                IMapper mapper)
        {
            this.userFollowingQueryRepository = userFollowingQueryRepository;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }
        public async Task<Result<List<UserViewModel>>> Handle(FollowersRequest request, CancellationToken cancellationToken)
        {
            var userFollowings = await this.userFollowingQueryRepository
            .GetAllAsync(e => e.TargetId == request.UserId, cancellationToken, nameof(Domain.Models.UserFollowing.Observer));
            if (userFollowings.Any())
                return Result<List<UserViewModel>>.SetSuccess(mapper.Map<List<UserViewModel>>(userFollowings.Select(e => e.Observer))!);
            else
                return Result<List<UserViewModel>>.SetSuccess(mapper.Map<List<UserViewModel>>(new List<UserViewModel>())!);
        }
    }
}