
using System.Globalization;
using System.Linq.Expressions;
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Activities.Query
{
    public class ActivityQueryRequest : IRequest<Result<PaginationViewModel<IEnumerable<ActivityViewModel>, string?>>>
    {
        public string? EventDate { get; set; }

        private int limit = 10;
        public int? Limit
        {
            get { return limit; }
            set
            {
                if (value != null && value < 50)
                    limit = value.Value;
            }
        }
    }

    public class ActivityQueryRequestHandler
    : IRequestHandler<ActivityQueryRequest, Result<PaginationViewModel<IEnumerable<ActivityViewModel>, string?>>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ActivityQueryRequestHandler> logger;

        public ActivityQueryRequestHandler(IActivityQueryRepository activityQueryRepository,
        IMapper mapper, ILogger<ActivityQueryRequestHandler> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.activityQueryRepository = activityQueryRepository;
        }

        public async Task<Result<PaginationViewModel<IEnumerable<ActivityViewModel>, string?>>> Handle(ActivityQueryRequest request, CancellationToken cancellationToken)
        {
            var requestType = typeof(ActivityQueryRequest);
            var activityProps = typeof(Activity).GetProperties();

            var parameter = Expression.Parameter(typeof(Activity), "a");
            var constant1 = Expression.Constant(1);
            var constant2 = Expression.Constant(1);
            var defaultCondition = Expression.Equal(constant1, constant2);

            foreach (var prop in requestType.GetProperties())
            {
                var value = prop.GetValue(request);
                var activityProp = activityProps.FirstOrDefault(e => e.Name == prop.Name);
                if (value == null || activityProp == null)
                    continue;

                var leftPart = Expression.PropertyOrField(parameter, prop.Name);
                if (prop.PropertyType != activityProp.PropertyType)
                {
                    if (activityProp.PropertyType == typeof(DateTime))
                    {
                        value = DateTime.Parse(value.ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    }
                    else
                        value = Convert.ChangeType(value, activityProp.PropertyType);
                }
                var rightPart = Expression.Constant(value, activityProp.PropertyType);
                var condition = Expression.LessThan(leftPart, rightPart);

                defaultCondition = Expression.AndAlso(defaultCondition, condition);
            }


            var defaultFilter = Expression.Lambda<Func<Activity, bool>>(defaultCondition, parameter);

            Func<IQueryable<Activity>, IOrderedQueryable<Activity>> orderby = (q) => q.OrderByDescending(e => e.EventDate);
            var result = await this.activityQueryRepository.GetAllAsync(defaultFilter, orderby, request.Limit, cancellationToken);
            var activities = await result.ProjectTo<ActivityViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

            var paginationViewModel = new PaginationViewModel<IEnumerable<ActivityViewModel>, string?>
            {
                Result = activities,
                Cursor = activities.Any() ? activities.Last().EventDate : null
            };

            return Result<PaginationViewModel<IEnumerable<ActivityViewModel>, string?>>.SetSuccess(paginationViewModel);
        }
    }
}