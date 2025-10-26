
using Application.Comment.Command;
using Application.Comment.Query;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR
{
    public class CommentHub : Hub
    {
        private const string AllActivityComments = "AllActivityComments";
        private const string CommentAdded = "CommentAdded";
        private readonly IMediator mediator;

        public CommentHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var activityid = httpContext?.Request.Query["activityid"];

            if (string.IsNullOrWhiteSpace(activityid))
                throw new HubException("Activity id not found");
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, activityid!);

                var result = await mediator.Send(new CommentQueryRequest { ActivityId = activityid! });

                await Clients.Caller.SendAsync(AllActivityComments, result);
            }
        }

        public async Task CreateComment(CommentCommandViewModel commentCommandViewModel)
        {
            var result = await mediator.Send(new CommentCommandRequest
            {
                Comment = commentCommandViewModel
            });

            await Clients.Group(commentCommandViewModel.ActivityId.ToString())
            .SendAsync(CommentAdded, result);
        }
    }
}