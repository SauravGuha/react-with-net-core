

using Api.Filters;
using Application.Activities.Command;
using Application.Activities.Query;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ActivityController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllActivities([FromQuery] string? title, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityQueryRequest() { Title = title }, cancellationToken);
            return this.ReturnResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityById([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityQueryByIdRequest() { Id = id }, cancellationToken);
            return this.ReturnResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCommandViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityCommandRequest { Activity = viewModel }, cancellationToken);
            return this.ReturnResult(result);
        }

        [HttpPut]
        [TypeFilter(typeof(ActivityUpdateFilter))]
        public async Task<IActionResult> UpdateActivity([FromQuery] Guid id, [FromBody] ActivityCommandViewModel viewModel,
         CancellationToken cancellationToken)
        {
            var result = await this.Mediator.Send(new ActivityCommandRequest { Id = id, Activity = viewModel }, cancellationToken);
            return this.ReturnResult(result);
        }

        [HttpDelete]
        [TypeFilter(typeof(ActivityUpdateFilter))]
        public async Task<IActionResult> DeleteActivity([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var result = await this.Mediator.Send(new ActivityDeleteCommand { Id = id }, cancellationToken);
            return this.ReturnResult(result);
        }
    }
}