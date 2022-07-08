using System;
using System.Threading.Tasks;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaRefresh
    {
        public Task Hold(Guid queueId);
        public Task Load(Guid queueId);
    }
}