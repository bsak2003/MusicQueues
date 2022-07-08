using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummyRefresh : IMediaRefresh
    {
        private readonly ILogger<DummyMediaPlayer> _logger;
        private readonly IRepository<Queue> _repository;
        
        public DummyRefresh(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async Task Hold(Guid queueId)
        {
            PlayerBackgroundTask.Stop(queueId);
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.OnHold);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Queue {queueId} put on hold");
        }

        public async Task Load(Guid queueId)
        {
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Playing);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Restored playback in queue {queueId} from hold");
        }
    }
}