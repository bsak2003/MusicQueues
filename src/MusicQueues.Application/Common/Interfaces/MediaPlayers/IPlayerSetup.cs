using System;
using System.Threading.Tasks;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IPlayerSetup
    {
        public Task<Uri> Setup(Guid queueId);
    }
}