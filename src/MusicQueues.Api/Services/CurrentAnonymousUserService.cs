using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public class CurrentAnonymousUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAnonymousUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies["AnonymousUserId"];
        }
    }
}