

using Application.Activities.Command;
using Application.Activities.Query;
using Application.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ActivityController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllActivities(CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityById([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityQueryRequest() { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCommandViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityCommandRequest { Activity = viewModel });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActivity([FromQuery] Guid id, [FromBody] ActivityCommandViewModel viewModel,
         CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityCommandRequest { Id = id, Activity = viewModel }, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivity([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var result = await this.Mediator.Send(new ActivityDeleteCommand { Id = id }, cancellationToken);
            return Ok(result);
        }
    }
}