using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.DeleteElement
{
    public class DeleteElementHandler : IRequestHandler<DeleteElement>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;

        public DeleteElementHandler(IRepository<Queue> queueRepository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _queueRepository = queueRepository;
            _mediaPlayers = mediaPlayers;
        }

        public async Task<Unit> Handle(DeleteElement request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            
            queue.RemoveElement(queue.Elements.First(x => x.Id == request.ElementId));
            
            mp.SongRemoved(request.QueueId, request.ElementId);
            await _queueRepository.Update(queue);
            
            return Unit.Value;
        }
    }
}