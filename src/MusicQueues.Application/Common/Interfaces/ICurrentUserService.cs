using System;

namespace MusicQueues.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid GetUserId();
    }
}