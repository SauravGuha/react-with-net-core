

using Application.Core;
using Domain.Infrastructure;
using Domain.Models;
using Domain.Repositories.UserRepository;
using MediatR;

namespace Application.UserFollowing.Command
{
    public class UserFollowingCommandRequest : IRequest<Result<Unit>>
    {
        public required string TargetId { get; set; }

        public required bool IsFollowing { get; set; }
    }

    public class UserFollowingCommandHandler : IRequestHandler<UserFollowingCommandRequest, Result<Unit>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IUserFollowingCommandRepository userFollowingCommandRepository;
        private readonly IUserFollowingQueryRepository userFollowingQueryRepository;

        public UserFollowingCommandHandler(IUserAccessor userAccessor,
        IUserFollowingCommandRepository userFollowingCommandRepository,
        IUserFollowingQueryRepository userFollowingQueryRepository)
        {
            this.userFollowingQueryRepository = userFollowingQueryRepository;
            this.userAccessor = userAccessor;
            this.userFollowingCommandRepository = userFollowingCommandRepository;
        }

        public async Task<Result<Unit>> Handle(UserFollowingCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userAccessor.GetUserAsync();

            if (user.Id == request.TargetId && request.IsFollowing)
            {
                return Result<Unit>.SetError("User cannot follow himself", 405);
            }
            else
            {
                var followingRecord = await this.userFollowingQueryRepository
                .GetAllAsync(e => e.ObserverId == user.Id && e.TargetId == request.TargetId, cancellationToken);
                if (followingRecord?.Any() == false)
                {
                    await this.userFollowingCommandRepository.CreateAsync(new Domain.Models.UserFollowing
                    {
                        ObserverId = user.Id,
                        TargetId = request.TargetId
                    }, cancellationToken);
                }
                else
                {
                    var record = followingRecord?.FirstOrDefault();
                    if (record != null)
                    {
                        await this.userFollowingCommandRepository.DeleteAsync(record, cancellationToken);
                    }
                }

                return Result<Unit>.SetSuccess(Unit.Value);
            }
        }
    }
}