using System;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaPlayback
    {
        public void Start(Guid queueId);
        public void Stop(Guid queueId);
    }
}