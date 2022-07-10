using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify.Persistence;

public class SpotifyQueueRepository : IRepository<SpotifyQueue>
{
    private readonly List<SpotifyQueue> _repository = new List<SpotifyQueue>();
    
    public Task<SpotifyQueue> ReadById(Guid id)
    {
        return Task.FromResult(_repository.Find(x => x.QueueId == id));
    }

    public Task<IEnumerable<SpotifyQueue>> ReadAll()
    {
        return Task.FromResult(_repository.AsEnumerable());
    }

    public Task Create(SpotifyQueue entity)
    {
        _repository.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(SpotifyQueue entity)
    {
        _repository[_repository.IndexOf(entity)] = entity;
        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        _repository.Remove(_repository.Find(x => x.QueueId == id));
        return Task.CompletedTask;
    }
}