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

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.Name;
        }
    }
}