

using System.Net;
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
            IEnumerable<Activity>? result = null;
            if (request.Id == null)
                result = await this.activityQueryRepository
                .GetAllAsync(null, cancellationToken, nameof(Activity.Attendees), $"{nameof(Activity.Attendees)}.User");
            else
            {
                var activity = await this.activityQueryRepository.GetById(request.Id.GetValueOrDefault(), cancellationToken, nameof(Activity.Attendees));
                if (activity != null)
                    result = new List<Activity> { activity };
                else
                    return Result<IEnumerable<ActivityViewModel>>.SetError("Key not found", (int)HttpStatusCode.NotFound);
            }
            return Result<IEnumerable<ActivityViewModel>>.SetSuccess(mapper.Map<IEnumerable<ActivityViewModel>>(result!)!);
        }
    }
}