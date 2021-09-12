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

        public DeleteQueueHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(DeleteQueue request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.Id);
            
            await _queueRepository.Delete(request.Id);
            
            return Unit.Value;
        }
    }
}