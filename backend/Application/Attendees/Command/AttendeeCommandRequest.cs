

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Repositories.AttendeeRespository;
using MediatR;

namespace Application.Attendees.Command
{
    public class AttendeeCommandRequest : IRequest<Result<AttendeeViewModel>>
    {
        public required AttendeeCommandViewModel AttendeeCommandViewModel { get; set; }
    }

    public class AttendeeCommandRequestHandler : IRequestHandler<AttendeeCommandRequest, Result<AttendeeViewModel>>
    {
        private readonly IAttendeeCommandRepository attendeeCommandRepository;
        private readonly IAttendeeQueryRepository attendeeQueryRepository;
        private readonly IMapper mapper;

        public AttendeeCommandRequestHandler(IAttendeeCommandRepository attendeeCommandRepository, IAttendeeQueryRepository attendeeQueryRepository,
        IMapper mapper)
        {
            this.attendeeCommandRepository = attendeeCommandRepository;
            this.attendeeQueryRepository = attendeeQueryRepository;
            this.mapper = mapper;
        }
        public async Task<Result<AttendeeViewModel>> Handle(AttendeeCommandRequest request, CancellationToken cancellationToken)
        {
            var requestInfo = request.AttendeeCommandViewModel;
            var attendeeInfos = await this.attendeeQueryRepository.GetAllAsync(e => e.UserId == requestInfo.UserId
            && e.ActivityId.ToString() == requestInfo.ActivityId, cancellationToken);

            var attendeeInfo = attendeeInfos.FirstOrDefault();
            if (attendeeInfo != null)
            {
                this.mapper.Map(requestInfo, attendeeInfo);
                await this.attendeeCommandRepository.UpdateAsync(attendeeInfo, cancellationToken);
                return Result<AttendeeViewModel>.SetSuccess(mapper.Map<AttendeeViewModel>(attendeeInfo)!);
            }
            else
            {
                var attendeObj = this.mapper.Map<Domain.Models.Attendees>(requestInfo);
                var result = await this.attendeeCommandRepository.CreateAsync(attendeObj!, cancellationToken);
                return Result<AttendeeViewModel>.SetSuccess(mapper.Map<AttendeeViewModel>(result)!);
            }
        }
    }
}