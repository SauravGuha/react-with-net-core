

using System.Linq.Expressions;
using System.Net;
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities.Query
{
    public class ActivityQueryRequest : IRequest<Result<IEnumerable<ActivityViewModel>>>
    {
        public string? Title { get; set; }
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
            var requestType = typeof(ActivityQueryRequest);

            var parameter = Expression.Parameter(typeof(Activity), "a");
            var constant1 = Expression.Constant(1);
            var constant2 = Expression.Constant(1);
            var defaultCondition = Expression.Equal(constant1, constant2);

            var defaultFilter = Expression.Lambda<Func<Activity, bool>>(defaultCondition, parameter);
            foreach (var prop in requestType.GetProperties())
            {
                var value = prop.GetValue(request);
                if (value != null)
                {
                    var property = Expression.Property(parameter, prop.Name);
                    var propertyConstant = Expression.Constant(value);
                    var condition = Expression.Equal(property, propertyConstant);
                    defaultCondition = Expression.AndAlso(defaultCondition, condition);
                    defaultFilter = Expression.Lambda<Func<Activity, bool>>(defaultCondition, parameter);
                }
            }

            // IEnumerable<Activity> result = await this.activityQueryRepository
            //     .GetAllAsync(defaultFilter, cancellationToken, nameof(Activity.Attendees), $"{nameof(Activity.Attendees)}.User");
            // return Result<IEnumerable<ActivityViewModel>>.SetSuccess(mapper.Map<IEnumerable<ActivityViewModel>>(result)!);

            var result = await this.activityQueryRepository.GetAllAsync(defaultFilter, cancellationToken);
            var activities = await result.ProjectTo<ActivityViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();
            return Result<IEnumerable<ActivityViewModel>>.SetSuccess(activities);
        }
    }
}