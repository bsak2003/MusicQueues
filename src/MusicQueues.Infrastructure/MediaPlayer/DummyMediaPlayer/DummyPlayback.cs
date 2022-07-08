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
    public class DummyPlayback : IMediaPlayback
    {
        private readonly ILogger<DummyMediaPlayer> _logger;
        private readonly IRepository<Queue> _repository;

        public DummyPlayback(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async Task Start(Guid queueId)
        {
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Playing);
            await _repository.Update(queue);
            
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
            _logger.LogInformation($"Started playback of queue {queueId}");
        }

        public async Task Stop(Guid queueId)
        {
            PlayerBackgroundTask.Stop(queueId);
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Stopped);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Stopped playback of queue {queueId}");
        }
    }
}