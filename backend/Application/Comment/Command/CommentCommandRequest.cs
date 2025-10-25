
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Comment.Command
{
    public class CommentCommandRequest : IRequest<Result<CommentViewModel>>
    {
        public required CommentCommandViewModel Comment { get; set; }
    }

    public class CommentCommandHandler : IRequestHandler<CommentCommandRequest, Result<CommentViewModel>>
    {
        private readonly IUserAccessor userAccessor;
        private readonly IMapper mapper;
        private readonly ICommentCommandRepository commentCommandRepository;

        public CommentCommandHandler(IUserAccessor userAccessor, IMapper mapper, ICommentCommandRepository commentCommandRepository)
        {
            this.userAccessor = userAccessor;
            this.mapper = mapper;
            this.commentCommandRepository = commentCommandRepository;
        }
        public async Task<Result<CommentViewModel>> Handle(CommentCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();
            var newComment = mapper.Map<Domain.Models.Comment>(request.Comment);
            newComment!.UserId = user.Id;

            var result = await commentCommandRepository.CreateAsync(newComment, cancellationToken);

            if (result == null)
                return Result<CommentViewModel>.SetError("Failed to add comment", 500);
            else
                return Result<CommentViewModel>.SetSuccess(mapper.Map<CommentViewModel>(result)!);
        }
    }
}