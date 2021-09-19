using System;
using System.Threading.Tasks;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaPlayerSelector
    {
        public IMediaPlayer FromPlatform(Platform platform);
        public IMediaPlayer FromQueue(Queue queue);
        public Task<IMediaPlayer> FromQueueId(Guid queueId);
    }
}