

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Query
{
    public class ActivityQueryRequest : IRequest<Result<IEnumerable<ActivityViewModel>>>
    {
        public Guid? Id { get; set; }
    }

    public class ActivityQueryRequestHandler : IRequestHandler<ActivityQueryRequest, Result<IEnumerable<ActivityViewModel>>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;

        public ActivityQueryRequestHandler(IActivityQueryRepository activityQueryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.activityQueryRepository = activityQueryRepository;
        }

        public async Task<Result<IEnumerable<ActivityViewModel>>> Handle(ActivityQueryRequest request, CancellationToken cancellationToken)
        {
            List<Activity> result = new List<Activity>();
            if (request.Id == null)
                result.AddRange(await this.activityQueryRepository.GetAllAsync(null, cancellationToken));
            else
            {
                var activity = await this.activityQueryRepository.GetById(request.Id.GetValueOrDefault(), cancellationToken);
                if (activity != null)
                    result.AddRange(activity);
                else
                    return Result<IEnumerable<ActivityViewModel>>.Failure($"{request.Id} not found", 404);
            }
            var handlerResult = mapper.Map<IEnumerable<ActivityViewModel>>(result)!;
            return Result<IEnumerable<ActivityViewModel>>.Success(handlerResult);
        }
    }
}