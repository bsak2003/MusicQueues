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

        public DeleteElementHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(DeleteElement request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            
            queue.RemoveElement(queue.Elements.First(x => x.Id == request.ElementId));
            await _queueRepository.Update(queue);
            
            return Unit.Value;
        }
    }
}