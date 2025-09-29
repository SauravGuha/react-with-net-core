

using System.Net;
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.ActivityRepository;
using MediatR;

namespace Application.Activities.Query
{
    public class ActivityQueryByIdRequest : IRequest<Result<ActivityViewModel>>
    {
        public Guid Id { get; set; }
    }

    public class ActivityQueryByIdRequestHandler : IRequestHandler<ActivityQueryByIdRequest, Result<ActivityViewModel>>
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        private readonly IMapper mapper;

        public ActivityQueryByIdRequestHandler(IActivityQueryRepository activityQueryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.activityQueryRepository = activityQueryRepository;
        }

        public async Task<Result<ActivityViewModel>> Handle(ActivityQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var activity = await this.activityQueryRepository.GetById(request.Id, cancellationToken, nameof(Activity.Attendees));
            if (activity != null)
                return Result<ActivityViewModel>.SetSuccess(mapper.Map<ActivityViewModel>(activity)!);
            else
                return Result<ActivityViewModel>.SetError("Key not found", (int)HttpStatusCode.NotFound);
        }
    }
}