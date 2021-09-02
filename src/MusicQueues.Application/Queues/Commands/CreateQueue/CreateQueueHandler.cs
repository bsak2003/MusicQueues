using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.CreateQueue
{
    public class CreateQueueHandler : IRequestHandler<CreateQueue, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Queue> _queueRepository;

        public CreateQueueHandler(ICurrentUserService currentUserService, IRepository<Queue> queueRepository)
        {
            _currentUserService = currentUserService;
            _queueRepository = queueRepository;
        }

        public async Task<Guid> Handle(CreateQueue request, CancellationToken cancellationToken)
        {
            var queue = new Queue(request.Platform);
            queue.AddMember(new QueueMember(_currentUserService.GetUserId().ToString(), MemberRole.Owner));
            await _queueRepository.Create(queue);
            return queue.Id;
        }
    }
}