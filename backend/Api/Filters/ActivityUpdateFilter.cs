

using Domain.Infrastructure;
using Domain.Repositories.ActivityRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ActivityUpdateFilter : IAuthorizationFilter
    {
        private readonly IUserAccessor userAccessor;
        private readonly IActivityQueryRepository activityQueryRepository;

        public ActivityUpdateFilter(IUserAccessor userAccessor, IActivityQueryRepository activityQueryRepository)
        {
            this.activityQueryRepository = activityQueryRepository;
            this.userAccessor = userAccessor;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = this.userAccessor.GetUserId();
            var controller = context.RouteData.Values["Controller"]?.ToString() ?? "";
            var action = context.RouteData.Values["Action"]?.ToString() ?? "";

            if (controller.ToLower() == "activity" && action.ToLower() == "updateactivity")
            {
                var id = context.HttpContext.Request.Query["id"].ToString();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var activities = activityQueryRepository.GetAllAsync(e => e.Id.ToString() == id, CancellationToken.None, "Attendees.User")
                    .GetAwaiter().GetResult();
                    if (activities.Any())
                    {
                        var first = activities.First();
                        var isHost = first.Attendees.Any(e => e.UserId == userId && e.IsHost);
                        if (!isHost)
                        {
                            context.Result = new UnauthorizedObjectResult($"Activity {id} cannot be edited by {userId}");
                            return;
                        }
                    }
                }
            }
        }
    }
}