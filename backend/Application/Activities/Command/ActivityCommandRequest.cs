

using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Command
{
    public class ActivityCommandRequest : IRequest<IEnumerable<ActivityViewModel>>
    {
        public Guid? Id { get; set; }
        public required ActivityCommandViewModel Activity { get; set; }
    }

    public class ActivityCommandRequestHandler : IRequestHandler<ActivityCommandRequest, IEnumerable<ActivityViewModel>>
    {
        private readonly IActivityCommandRepository activityCommand;
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;
        public ActivityCommandRequestHandler(IActivityCommandRepository activityCommand, IMapper mapper,
        IActivityQueryRepository activityQueryRepository)
        {
            this.mapper = mapper;
            this.activityCommand = activityCommand;
            this.activityQueryRepository = activityQueryRepository;

        }
        public async Task<IEnumerable<ActivityViewModel>> Handle(ActivityCommandRequest request, CancellationToken cancellationToken)
        {
            var result = new List<ActivityViewModel>();
            //add
            if (request.Id == null)
            {
                var entity = mapper.Map<Activity>(request.Activity);
                var activities = await this.activityQueryRepository.GetAllAsync(e => e.EventDate.DayOfYear == entity!.EventDate.DayOfYear
                && e.Category.ToLower() == entity.Category.ToLower() && e.Venue.ToLower() == entity.Venue.ToString()
                && e.City.ToLower() == entity.City.ToLower(),
                 cancellationToken);
                if (activities.Any())
                    throw new InvalidOperationException("Cannot create event on the same date, same venue");
                else
                {
                    var newActivity = await activityCommand.CreateAsync(entity!, cancellationToken);
                    result.Add(mapper.Map<ActivityViewModel>(newActivity!)!);
                }
            }
            //update
            else
            {
                var activity = await this.activityQueryRepository.GetById(request.Id.GetValueOrDefault(), cancellationToken);
                if (activity == null)
                    throw new KeyNotFoundException($"{request.Id} not found");
                else
                {
                    mapper.Map(request.Activity, activity);
                    await activityCommand.UpdateAsync(activity!, cancellationToken);
                    result.Add(mapper.Map<ActivityViewModel>(activity!)!);
                }
            }

            return result;
        }
    }
}