using System;
using System.Threading.Tasks;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaPlayback
    {
        public Task Start(Guid queueId);
        public Task Stop(Guid queueId);
    }
}