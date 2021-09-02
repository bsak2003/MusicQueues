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
            var queue = await _queueRepository.ReadById(request.Id);
            
            if(string.IsNullOrWhiteSpace(request.Title))
                queue.UpdateTitle(request.Title);
            if(string.IsNullOrWhiteSpace(request.Description))
                queue.UpdateDescription(request.Description);

            await _queueRepository.Update(queue);
            return Unit.Value;
        }
    }
}