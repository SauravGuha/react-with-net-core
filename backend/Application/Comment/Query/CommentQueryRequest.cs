

using System.Linq.Expressions;
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Repositories.PhotoRepository;
using MediatR;

namespace Application.Comment.Query
{
    public class CommentQueryRequest : IRequest<Result<List<CommentViewModel>>>
    {
        public required string ActivityId { get; set; }
    }

    public class CommentQueryHandler : IRequestHandler<CommentQueryRequest, Result<List<CommentViewModel>>>
    {
        private readonly ICommentQueryRepository commentQueryRepositor;
        private readonly IMapper mapper;

        public CommentQueryHandler(ICommentQueryRepository commentQueryRepositor, IMapper mapper)
        {
            this.commentQueryRepositor = commentQueryRepositor;
            this.mapper = mapper;
        }

        public async Task<Result<List<CommentViewModel>>> Handle(CommentQueryRequest request, CancellationToken cancellationToken)
        {
            var constant1 = Expression.Constant(1);
            var constant2 = Expression.Constant(1);
            var condition = Expression.Equal(constant1, constant2);
            var param = Expression.Parameter(typeof(Domain.Models.Comment), "e");
            var lambda = Expression.Lambda<Func<Domain.Models.Comment, bool>>(condition, param);

            if (Guid.TryParse(request.ActivityId, out var activityId))
            {
                var condition2 = Expression.Equal(Expression.Property(param, nameof(Domain.Models.Comment.ActivityId)),
                                Expression.Constant(activityId)
                            );

                condition = Expression.AndAlso(condition, condition2);
                lambda = Expression.Lambda<Func<Domain.Models.Comment, bool>>(condition, param);
            }


            var comments = await commentQueryRepositor.GetAllAsync(lambda, cancellationToken, "User");
            var orderedcomments = comments.OrderByDescending(c => c.CreatedAt).ToList();
            return Result<List<CommentViewModel>>.SetSuccess(mapper.Map<List<CommentViewModel>>(orderedcomments)!);
        }
    }
}