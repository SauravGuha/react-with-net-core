

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
    }
}