

using System.Net;
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
            if (result.Status) return Ok(result);

            if (result.ErrorCode == (int)HttpStatusCode.NotFound) return NotFound(result);

            if (result.ErrorCode == (int)HttpStatusCode.Conflict) return Conflict(result);

            return BadRequest();
        }
    }
}