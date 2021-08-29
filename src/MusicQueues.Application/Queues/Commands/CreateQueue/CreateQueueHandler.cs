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

        public CreateQueueHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task<Guid> Handle(CreateQueue request, CancellationToken cancellationToken)
        {
            var queue = new Queue(request.Platform);
            queue.AddMember(new QueueMember(_currentUserService.GetUserId(), MemberRole.Owner));
            throw new NotImplementedException();
        }
    }
}