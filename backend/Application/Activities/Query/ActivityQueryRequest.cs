

using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Query
{
    public class ActivityQueryRequest : IRequest<IEnumerable<ActivityViewModel>>
    {
        public Guid? Id { get; set; }
    }

    public class ActivityQueryRequestHandler : IRequestHandler<ActivityQueryRequest, IEnumerable<ActivityViewModel>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;

        public ActivityQueryRequestHandler(IActivityQueryRepository activityQueryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.activityQueryRepository = activityQueryRepository;
        }

        public async Task<IEnumerable<ActivityViewModel>> Handle(ActivityQueryRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Activity> result = null;
            if (request.Id == null)
                result = await this.activityQueryRepository.GetAllAsync(null, cancellationToken);
            else
            {
                var activity = await this.activityQueryRepository.GetById(request.Id.GetValueOrDefault(), cancellationToken);
                if (activity != null)
                    result = new List<Activity> { activity };
                else
                    throw new KeyNotFoundException($"{request.Id} not found");
            }
            return mapper.Map<IEnumerable<ActivityViewModel>>(result!);
        }
    }
}