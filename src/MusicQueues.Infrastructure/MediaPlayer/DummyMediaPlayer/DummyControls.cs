using System;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummyControls : IPlayerControls
    {
        private readonly ILogger<DummyMediaPlayer> _logger;
        private readonly IRepository<Queue> _repository;

        public DummyControls(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async void Pause(Guid queueId)
        {
            PlayerBackgroundTask.Stop(queueId);
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Paused);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Playback in queue {queueId} paused");
        }

        public async void Unpause(Guid queueId)
        {
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Playing);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Playback in queue {queueId} unpaused");
        }
    }
}