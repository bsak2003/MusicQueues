using System;
using System.Threading.Tasks;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IPlayerControls
    {
        public Task Pause(Guid queueId);
        public Task Unpause(Guid queueId);
    }
}