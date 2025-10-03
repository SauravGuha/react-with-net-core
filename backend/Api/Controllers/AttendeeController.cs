

using Api.Filters;
using Application.Attendees.Command;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AttendeeController : BaseController
    {
        [HttpPut]
        [TypeFilter(typeof(AttendenceFilter))]
        public async Task<IActionResult> CreateUpdateAttendee([FromBody] AttendeeCommandViewModel attendeeCommandViewModel, CancellationToken token)
        {
            var result = await this.Mediator.Send(new AttendeeCommandRequest { AttendeeCommandViewModel = attendeeCommandViewModel }, token);
            return this.ReturnResult(result);
        }
    }
}