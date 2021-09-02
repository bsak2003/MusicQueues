using System;
using Microsoft.AspNetCore.Http;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public class CurrentAnonymousUserService : ICurrentUserService
    {
        public const string CookieName = "UserId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAnonymousUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                return _httpContextAccessor.HttpContext?.Request.Cookies[CookieName];
            }

            var userId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, userId);
            return userId;
        }
    }
}