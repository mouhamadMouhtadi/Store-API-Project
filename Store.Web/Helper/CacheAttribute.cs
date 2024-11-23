using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CachService;
using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute,IAsyncActionFilter
    {
        private int _timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheServices = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            var cachKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await _cacheServices.GetCacheResponseAsync(cachKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var ContentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = ContentResult;
                return;
            }
            var executeContext = await next();
            if (executeContext.Result is OkObjectResult response)
            {
                await _cacheServices.SetCacheResponseAsync(cachKey, response.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder cachKey = new StringBuilder();
            cachKey.Append($"{request.Path}");
            foreach (var ( Key,Value) in request.Query.OrderBy(k=>k.Key ))
            {
                cachKey.Append($"{Key}-{Value}");
            }
            return cachKey.ToString();
        }
    }
}
