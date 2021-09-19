using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.PlayQueue
{
    public class PlayQueueHandler : IRequestHandler<PlayQueue>
    {
        private readonly IRepository<Queue> _repository;
        private readonly IMediaPlayerSelector _selector;

        public PlayQueueHandler(IRepository<Queue> repository, IEnumerable<IMediaPlayer> mediaPlayers, IMediaPlayerSelector selector)
        {
            _repository = repository;
            _selector = selector;
        }
        
        public async Task<Unit> Handle(PlayQueue request, CancellationToken cancellationToken)
        {
            var queue = await _repository.ReadById(request.QueueId);

            queue.UpdateStatus(Status.Playing);
            
            _selector.FromQueue(queue).Playback.Start(queue.Id);
            await _repository.Update(queue);

            return Unit.Value;
        }
    }
}