

using System.Linq.Expressions;
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
    public class ActivityByFilterForUserRequest : IRequest<Result<List<EventViewModel>>>
    {
        public required string Filter { get; set; }

        public required string UserId { get; set; }
    }

    public class ActivityByFilterForUserRequestHandler : IRequestHandler<ActivityByFilterForUserRequest, Result<List<EventViewModel>>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;

        public ActivityByFilterForUserRequestHandler(IActivityQueryRepository activityQueryRepository, IMapper mapper)
        {
            this.activityQueryRepository = activityQueryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<EventViewModel>>> Handle(ActivityByFilterForUserRequest request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            //future, past IsAttending, hosting is hosting
            var defaultCondition = Expression.Equal(Expression.Constant(1), Expression.Constant(1));

            var activityParameter = Expression.Parameter(typeof(Activity), "a");
            var attendeePropExp = Expression.PropertyOrField(activityParameter, nameof(Activity.Attendees));

            var attendeesParameter = Expression.Parameter(typeof(Domain.Models.Attendees), "at");
            var userPropExp = Expression.PropertyOrField(attendeesParameter, nameof(Domain.Models.Attendees.UserId));
            var userCondition = Expression.Equal(userPropExp, Expression.Constant(request.UserId));
            //activityQuerable.Where(e => e.EventDate < DateTime.UtcNow && e.Attendees.Any(a => a.IsAttending == true && a.UserId == "user guid"));
            if (filter.ToLower() == "future" || filter.ToLower() == "past")
            {

                var attendingPropExp = Expression.PropertyOrField(attendeesParameter, nameof(Domain.Models.Attendees.IsAttending));
                userCondition = Expression.AndAlso(userCondition, Expression.Equal(attendingPropExp, Expression.Constant(true)));
                var anyMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Any" && m.GetParameters().Length == 2
                                            && m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>))
                                            .MakeGenericMethod(typeof(Domain.Models.Attendees));
                var innerLambda = Expression.Lambda(userCondition, attendeesParameter);
                var anyExp = Expression.Call(anyMethod, attendeePropExp, innerLambda);
                defaultCondition = Expression.AndAlso(defaultCondition, anyExp);

                var eventDateExpression = Expression.PropertyOrField(activityParameter, nameof(Activity.EventDate));
                var isFuture = filter.ToLower() == "future";
                var dateConstant = Expression.Constant(DateTime.UtcNow);
                defaultCondition = isFuture
                ? Expression.AndAlso(defaultCondition, Expression.GreaterThan(eventDateExpression, dateConstant))
                : Expression.AndAlso(defaultCondition, Expression.LessThan(eventDateExpression, dateConstant));
            }
            else if (filter.ToLower() == "hosting")
            {
                var attendingPropExp = Expression.PropertyOrField(attendeesParameter, nameof(Domain.Models.Attendees.IsHost));
                userCondition = Expression.AndAlso(userCondition, Expression.Equal(attendingPropExp, Expression.Constant(true)));
                var anyMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Any" && m.GetParameters().Length == 2
                                            && m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>))
                                            .MakeGenericMethod(typeof(Domain.Models.Attendees));
                var innerLambda = Expression.Lambda(userCondition, attendeesParameter);
                var anyExp = Expression.Call(anyMethod, attendeePropExp, innerLambda);
                defaultCondition = Expression.AndAlso(defaultCondition, anyExp);
            }
            var defaultFilter = Expression.Lambda<Func<Activity, bool>>(defaultCondition, activityParameter);
            var activitiesByFilter = await activityQueryRepository.GetAllAsync(defaultFilter, null, 50, cancellationToken);
            var events = await activitiesByFilter.ProjectTo<EventViewModel>(this.mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            return Result<List<EventViewModel>>.SetSuccess(events);
        }
    }
}