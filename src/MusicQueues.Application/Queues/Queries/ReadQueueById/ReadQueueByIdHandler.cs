using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Queues.Queries.Common;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Queries.ReadQueueById
{
    public class ReadQueueByIdHandler : IRequestHandler<ReadQueueById, QueueDto>
    {
        private readonly IRepository<Queue> _queueRepository;

        public ReadQueueByIdHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<QueueDto> Handle(ReadQueueById request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.Id);
            return new QueueDto(queue);
        }
    }
}