using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Commands.PlayQueue
{
    public class PlayQueueHandler : IRequestHandler<PlayQueue>
    {
        private readonly IRepository<Queue> _repository;
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;
        
        public PlayQueueHandler(IRepository<Queue> repository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _repository = repository;
            _mediaPlayers = mediaPlayers;
        }
        
        public async Task<Unit> Handle(PlayQueue request, CancellationToken cancellationToken)
        {
            var queue = await _repository.ReadById(request.QueueId);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            mp.StartPlayback(queue.Id);
            return Unit.Value;
        }
    }
}