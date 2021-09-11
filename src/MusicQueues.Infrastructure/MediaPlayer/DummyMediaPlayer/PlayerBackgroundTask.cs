using System.Threading.Tasks;
using MediatR;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class PlayerBackgroundTask
    {
        public PlayerBackgroundTask(IMediator mediator)
        {
        }

        public async Task Play()
        {

        }
    }
}