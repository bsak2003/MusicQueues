using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummySetup : IPlayerSetup
    {
        private ILogger<DummyMediaPlayer> _logger;
        private readonly IRepository<Queue> _repository;

        public DummySetup(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Uri> Setup(Guid queueId)
        {
            _logger.LogInformation($"Requested setup of queue {queueId}");
            
            var queue = await _repository.ReadById(queueId);
            queue.UpdateStatus(Status.Authenticated);
            await _repository.Update(queue);
            
            _logger.LogInformation($"Setup of queue {queueId} finished!");
            
            return new Uri("https://example.com/");
        }
    }
}