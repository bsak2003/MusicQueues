using System;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

public class SystemQueueStatus
{
    private readonly IRepository<Queue> _repository;

    public SystemQueueStatus(IRepository<Queue> repository)
    {
        _repository = repository;
    }

    public async Task Update(Guid queueId, Status queueStatus)
    {
        var queue = await _repository.ReadById(queueId);
        queue.UpdateStatus(queueStatus);
        await _repository.Update(queue);
    }
}