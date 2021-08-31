using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Queries.ReadQueueById
{
    public class ReadQueueByIdHandler : IRequestHandler<ReadQueueById, Queue>
    {
        private readonly IRepository<Queue> _queueRepository;

        public ReadQueueByIdHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public Task<Queue> Handle(ReadQueueById request, CancellationToken cancellationToken)
        {
            return _queueRepository.ReadById(request.Id);
        }
    }
}