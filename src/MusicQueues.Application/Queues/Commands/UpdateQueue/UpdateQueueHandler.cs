using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Commands.UpdateQueue
{
    public class UpdateQueueHandler : IRequestHandler<UpdateQueue>
    {
        private readonly IRepository<Queue> _queueRepository;

        public UpdateQueueHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(UpdateQueue request, CancellationToken cancellationToken)
        {
            await _queueRepository.Update(request.Queue);
            return Unit.Value;
        }
    }
}