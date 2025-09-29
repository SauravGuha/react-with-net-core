

using System.Linq.Expressions;
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

            IEnumerable<Activity> result = await this.activityQueryRepository
                .GetAllAsync(null, cancellationToken, nameof(Activity.Attendees), $"{nameof(Activity.Attendees)}.User");
            return Result<IEnumerable<ActivityViewModel>>.SetSuccess(mapper.Map<IEnumerable<ActivityViewModel>>(result)!);
        }
    }
}