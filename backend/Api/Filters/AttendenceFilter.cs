
using Application.ViewModels;
using Domain.Infrastructure;
using Domain.Repositories.ActivityRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class AttendenceFilter : IActionFilter
    {
        private readonly IUserAccessor userAccessor;
        private readonly IActivityQueryRepository activityQueryRepository;

        public AttendenceFilter(IUserAccessor userAccessor, IActivityQueryRepository activityQueryRepository)
        {
            this.userAccessor = userAccessor;
            this.activityQueryRepository = activityQueryRepository;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = this.userAccessor.GetUserId();
            var controller = context.RouteData.Values["Controller"]?.ToString() ?? "";
            var action = context.RouteData.Values["Action"]?.ToString() ?? "";
            if (controller.ToLower() == "attendee" && action.ToLower() == "createupdateattendee")
            {
                var attendeeViewModel = context.ActionArguments["AttendeeCommandViewModel"] as AttendeeCommandViewModel;
                if (context.ModelState.IsValid)
                {
                    var activityDetail = activityQueryRepository.GetById(Guid.Parse(attendeeViewModel!.ActivityId), CancellationToken.None, "Attendees.User")
                    .GetAwaiter().GetResult();
                    if (activityDetail != null)
                    {
                        var attendeeInfo = activityDetail.Attendees.FirstOrDefault(e => e.IsHost == true);
                        if (attendeeInfo!.UserId.ToString() == userId)
                        {
                            if (!attendeeViewModel.IsAttending)
                            {
                                context.Result = new BadRequestObjectResult("Hosting user cannot set isAttending to false");
                                return;
                            }
                            if (!attendeeViewModel.IsHost)
                            {
                                context.Result = new BadRequestObjectResult("Cannot change host of activity once created");
                                return;
                            }
                        }
                    }
                    else
                    {
                        context.Result = new NotFoundObjectResult("Avtivity not found");
                        return;
                    }
                }
            }
        }
    }
}