
using Application.Core;
using Application.ViewModels;
using AutoMapper;
using Domain.Infrastructure;
using MediatR;

namespace Application.LocationIq
{
    public class LocationIqAutoCompleteRequest : IRequest<Result<List<LocationIqViewModel>>>
    {
        public required string Address { get; set; }
    }

    public class LocationIqAutoCompleteRequestHandler : IRequestHandler<LocationIqAutoCompleteRequest, Result<List<LocationIqViewModel>>>
    {
        private readonly ILocationService locationService;
        private readonly IMapper mapper;
        public LocationIqAutoCompleteRequestHandler(ILocationService locationService, IMapper mapper)
        {
            this.mapper = mapper;
            this.locationService = locationService;

        }
        public async Task<Result<List<LocationIqViewModel>>> Handle(LocationIqAutoCompleteRequest request, CancellationToken cancellationToken)
        {
            var result = await locationService.AutoCompleteAsync(request.Address);
            if (request != null)
            {
                var returnResult = mapper.Map<List<LocationIqViewModel>>(result);
                return Result<List<LocationIqViewModel>>.SetSuccess(returnResult!);
            }
            else
            {
                return Result<List<LocationIqViewModel>>.SetSuccess(new List<LocationIqViewModel>());
            }
        }
    }
}