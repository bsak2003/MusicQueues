using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.UpdateElement
{
    public class MoveElementHandler : IRequestHandler<MoveElement>
    {
        private readonly IRepository<Queue> _queueRepository;

        public MoveElementHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(MoveElement request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            queue.MoveElement(queue.Elements.First(x => x.Id == request.ElementId), request.NewPosition);
            await _queueRepository.Update(queue);
            return Unit.Value;
        }
    }
}