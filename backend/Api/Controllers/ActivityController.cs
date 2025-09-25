

using Domain.Repositories.ActivityRepository;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IActivityQueryRepository activityQueryRepository;
        public ActivityController(IActivityQueryRepository activityQueryRepository)
        {
            this.activityQueryRepository = activityQueryRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities(CancellationToken cancellationToken)
        {
            var result = await this.activityQueryRepository.GetAllAsync(null, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityById([FromQuery]Guid id, CancellationToken cancellationToken)
        {
            var result = await this.activityQueryRepository.GetById(id, cancellationToken);
            return Ok(result);
        }
    }
}