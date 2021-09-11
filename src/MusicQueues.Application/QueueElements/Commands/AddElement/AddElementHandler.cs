using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;

        public AddElementHandler(ICurrentUserService currentUserService, IRepository<Queue> queueRepository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _currentUserService = currentUserService;
            _queueRepository = queueRepository;
            _mediaPlayers = mediaPlayers;
        }
        
        public async Task<Guid> Handle(AddElement request, CancellationToken cancellationToken)
        {
            var element = new QueueElement(_currentUserService.GetUserId(), request.Reference, request.Title);
            var queue = await _queueRepository.ReadById(request.QueueId);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            
            queue.AddElement(element);
            
            mp.SongAdded(request.QueueId, element);
            await _queueRepository.Update(queue);
            
            return element.Id;
        }
    }
}