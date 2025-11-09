

using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using MediatR;

namespace Application.LocationIq
{
    public class LocationIqReverseRequest : IRequest<Result<LocationIqViewModel>>
    {
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }

    public class LocationIqReverseRequestHandler : IRequestHandler<LocationIqReverseRequest, Result<LocationIqViewModel>>
    {
        private readonly ILocationService locationService;
        private readonly IMapper mapper;
        public LocationIqReverseRequestHandler(ILocationService locationService, IMapper mapper)
        {
            this.mapper = mapper;
            this.locationService = locationService;

        }
        public async Task<Result<LocationIqViewModel>> Handle(LocationIqReverseRequest request, CancellationToken cancellationToken)
        {
            var result = await locationService.ReverseGeoCoding(request.Latitude, request.Longitude);
            var returnResult = mapper.Map<LocationIqViewModel>(result);
            return Result<LocationIqViewModel>.SetSuccess(returnResult);
        }
    }

}