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

        public Guid GetUserId()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                return Guid.Parse(_httpContextAccessor.HttpContext?.Request.Cookies[CookieName] ?? throw new InvalidOperationException());
            }
            else
            {
                var userId = Guid.NewGuid();
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, userId.ToString());
                return userId;
            }
        }
    }
}