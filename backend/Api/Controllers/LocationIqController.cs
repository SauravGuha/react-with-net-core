

using Application.LocationIq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class LocationIqController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAutoComplete([FromQuery] string address, CancellationToken token)
        {
            var result = await this.Mediator.Send(new LocationIqAutoCompleteRequest { Address = address }, token);
            return this.ReturnResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetReverse([FromQuery] double latitude, [FromQuery] double longitude, CancellationToken token)
        {
            var result = await this.Mediator.Send(new LocationIqReverseRequest { Latitude = latitude, Longitude = longitude }, token);
            return this.ReturnResult(result);
        }
    }
}