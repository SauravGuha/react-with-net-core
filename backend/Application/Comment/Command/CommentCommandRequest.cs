
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Comment.Command
{
    public class CommentCommandRequest : IRequest<Result<List<CommentViewModel>>>
    {
        public required CommentCommandViewModel Comment { get; set; }
    }

    public class CommentCommandHandler : IRequestHandler<CommentCommandRequest, Result<List<CommentViewModel>>>
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
        public async Task<Result<List<CommentViewModel>>> Handle(CommentCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();
            var newComment = mapper.Map<Domain.Models.Comment>(request.Comment);
            newComment!.UserId = user.Id;

            var result = await commentCommandRepository.CreateAsync(newComment, cancellationToken);

            if (result == null)
                return Result<List<CommentViewModel>>.SetError("Failed to add comment", 500);
            else
                return Result<List<CommentViewModel>>
                .SetSuccess(mapper.Map<List<CommentViewModel>>(new List<Domain.Models.Comment> { result })!);
        }
    }
}