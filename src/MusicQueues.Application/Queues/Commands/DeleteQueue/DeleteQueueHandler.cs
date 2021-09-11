using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Commands.DeleteQueue
{
    public class DeleteQueueHandler : IRequestHandler<DeleteQueue>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;

        public DeleteQueueHandler(IRepository<Queue> queueRepository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _queueRepository = queueRepository;
            _mediaPlayers = mediaPlayers;
        }

        public async Task<Unit> Handle(DeleteQueue request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.Id);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            
            mp.QueueRemoved(request.Id);
            await _queueRepository.Delete(request.Id);
            
            return Unit.Value;
        }
    }
}