

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Filters
{
    public class LocationIqFilter : IResourceFilter
    {
        private readonly IMemoryCache memoryCache;

        public LocationIqFilter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var queryKey = context.HttpContext.Request.QueryString.Value;
            if (!string.IsNullOrWhiteSpace(queryKey))
            {
                if (context.Result is OkObjectResult httpResult)
                    memoryCache.Set(queryKey, httpResult.Value);
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var queryKey = context.HttpContext.Request.QueryString.Value;
            object? prevValue = null;
            if (!string.IsNullOrWhiteSpace(queryKey) && this.memoryCache.TryGetValue(queryKey, out prevValue))
            {
                context.Result = new OkObjectResult(prevValue);
            }
        }
    }
}