using System;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Common.Interfaces
{
    public interface IMediaPlayer
    {
        public Platform Platform { get; }
        public void StartPlayback(Guid queueId);
        public void StopPlayback(Guid queueId);
    }
}