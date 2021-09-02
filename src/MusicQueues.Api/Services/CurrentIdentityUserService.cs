using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public class CurrentIdentityUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentIdentityUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException());
        }
    }
}