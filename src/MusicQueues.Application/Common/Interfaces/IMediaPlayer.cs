using System;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Common.Interfaces
{
    public interface IMediaPlayer
    {
        public Platform Platform { get; }
        public void QueueCreated(Queue queue);
        public void QueueRemoved(Guid queueId);
        public void SongAdded(Guid queueId, QueueElement element);
        public void SongMoved(Guid queueId, Guid songId, int position);
        public void SongRemoved(Guid queueId, Guid songId);
        public void RefreshQueue(Queue queue);
    }
}