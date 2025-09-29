
using System.Net;
using Application.Core;
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Command
{
    public class ActivityDeleteCommand : IRequest<Result<Unit>>
    {
        public required Guid Id { get; set; }
    }

    public class ActivityDeleteCommandHandler : IRequestHandler<ActivityDeleteCommand, Result<Unit>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IActivityCommandRepository activityCommandRepository;

        public ActivityDeleteCommandHandler(IActivityQueryRepository activityQueryRepository,
        IActivityCommandRepository activityCommandRepository)
        {
            this.activityQueryRepository = activityQueryRepository;
            this.activityCommandRepository = activityCommandRepository;
        }
        public async Task<Result<Unit>> Handle(ActivityDeleteCommand request, CancellationToken cancellationToken)
        {
            var activity = await activityQueryRepository.GetById(request.Id, cancellationToken);
            if (activity != null)
                await activityCommandRepository.DeleteAsync(activity, cancellationToken);
            else
                return Result<Unit>.SetError($"{request.Id} not found", (int)HttpStatusCode.NotFound);

            return Result<Unit>.SetSuccess(Unit.Value);
        }
    }
}