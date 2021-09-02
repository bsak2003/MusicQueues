using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.AddElement
{
    public class AddElementHandler : IRequestHandler<AddElement, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Queue> _queueRepository;

        public AddElementHandler(ICurrentUserService currentUserService, IRepository<Queue> queueRepository)
        {
            _currentUserService = currentUserService;
            _queueRepository = queueRepository;
        }
        
        public async Task<Guid> Handle(AddElement request, CancellationToken cancellationToken)
        {
            var element = new QueueElement(_currentUserService.GetUserId(), request.Reference, request.Title);
            var queue = await _queueRepository.ReadById(request.QueueId);
            queue.AddElement(element);
            await _queueRepository.Update(queue);
            return element.Id;
        }
    }
}