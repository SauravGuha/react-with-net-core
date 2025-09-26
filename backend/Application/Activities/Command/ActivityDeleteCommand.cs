
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Command
{
    public class ActivityDeleteCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
    }

    public class ActivityDeleteCommandHandler : IRequestHandler<ActivityDeleteCommand, Unit>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IActivityCommandRepository activityCommandRepository;

        public ActivityDeleteCommandHandler(IActivityQueryRepository activityQueryRepository,
        IActivityCommandRepository activityCommandRepository)
        {
            this.activityQueryRepository = activityQueryRepository;
            this.activityCommandRepository = activityCommandRepository;
        }
        public async Task<Unit> Handle(ActivityDeleteCommand request, CancellationToken cancellationToken)
        {
            var activity = await activityQueryRepository.GetById(request.Id, cancellationToken);
            if (activity != null)
            {
                await activityCommandRepository.DeleteAsync(activity, cancellationToken);
            }
            else
            {
                throw new KeyNotFoundException($"{request.Id} not found");
            }
            return Unit.Value;
        }
    }
}