using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.DeleteElement
{
    public class DeleteElementHandler : IRequestHandler<DeleteElement>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly IMediaPlayerSelector _selector;

        public DeleteElementHandler(IRepository<Queue> queueRepository, IMediaPlayerSelector selector)
        {
            _queueRepository = queueRepository;
            _selector = selector;
        }

        public async Task<Unit> Handle(DeleteElement request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);

            await _selector.FromQueue(queue).Refresh.Hold(queue.Id);
            
            queue.RemoveElement(queue.Elements.First(x => x.Id == request.ElementId));
            await _queueRepository.Update(queue);
            
            await _selector.FromQueue(queue).Refresh.Load(queue.Id);
            
            return Unit.Value;
        }
    }
}