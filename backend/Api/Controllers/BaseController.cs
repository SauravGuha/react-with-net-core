

using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator mediator = null!;
        public IMediator Mediator
        {
            get
            {
                mediator = mediator ?? this.HttpContext.RequestServices.GetRequiredService<IMediator>();
                return mediator;
            }
        }

        public IActionResult ReturnResult<T>(Result<T> result)
        {
            if (result.IsSuccess) return Ok(result);

            if (!result.IsSuccess && result.Code == 404) return NotFound(result);

            if (!result.IsSuccess && result.Code == 409) return Conflict(result);

            return BadRequest(result);
        }
    }
}