using System;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaRefresh
    {
        public void Hold(Guid queueId);
        public void Load(Guid queueId);
    }
}