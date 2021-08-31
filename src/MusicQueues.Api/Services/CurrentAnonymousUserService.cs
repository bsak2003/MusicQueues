using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public class CurrentAnonymousUserService : ICurrentUserService, IMiddleware
    {
        public const string CookieName = "UserId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAnonymousUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[CookieName];
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(!context.Request.Cookies.ContainsKey(CookieName))
            {
                context.Response.Cookies.Append(CookieName, Guid.NewGuid().ToString());
            }

            await next(context);
        }
    }
}