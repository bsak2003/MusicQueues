using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Common.Interfaces
{
    public interface IMediaPlayer
    {
        public void RefreshQueue(Queue queue);
    }
}