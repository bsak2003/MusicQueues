using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.Selector
{
    public class MediaPlayerSelector : IMediaPlayerSelector
    {
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;
        private readonly IRepository<Queue> _repository;

        public MediaPlayerSelector(IEnumerable<IMediaPlayer> mediaPlayers, IRepository<Queue> repository)
        {
            _mediaPlayers = mediaPlayers;
            _repository = repository;
        }

        public IMediaPlayer FromPlatform(Platform platform) => _mediaPlayers.First(x => x.Platform == platform);
        public IMediaPlayer FromQueue(Queue queue) => FromPlatform(queue.Platform);
        public async Task<IMediaPlayer> FromQueueId(Guid queueId) => FromQueue(await _repository.ReadById(queueId));
 
    }
}