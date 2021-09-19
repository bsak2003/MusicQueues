using System;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IPlayerControls
    {
        public void Pause(Guid queueId);
        public void Unpause(Guid queueId);
    }
}