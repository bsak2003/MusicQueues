using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.UpdateElement
{
    public class UpdateElementHandler : IRequestHandler<UpdateElement>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;

        public UpdateElementHandler(IRepository<Queue> queueRepository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _queueRepository = queueRepository;
            _mediaPlayers = mediaPlayers;
        }

        public async Task<Unit> Handle(UpdateElement request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            
            queue.MoveElement(queue.Elements.First(x => x.Id == request.ElementId), request.NewPosition);
            
            mp.SongMoved(request.QueueId, request.ElementId, request.NewPosition);
            await _queueRepository.Update(queue);
            
            return Unit.Value;
        }
    }
}