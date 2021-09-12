using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

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
            
            queue.UpdateStatus(Status.Playing);
            
            mp.StartPlayback(queue.Id);
            await _repository.Update(queue);

            return Unit.Value;
        }
    }
}