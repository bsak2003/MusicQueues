﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueElements.Commands.UpdateElement
{
    public class UpdateElementHandler : IRequestHandler<UpdateElement>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly IMediaPlayerSelector _selector;
        
        public UpdateElementHandler(IRepository<Queue> queueRepository, IMediaPlayerSelector selector)
        {
            _queueRepository = queueRepository;
            _selector = selector;
        }

        public async Task<Unit> Handle(UpdateElement request, CancellationToken cancellationToken)
        {
            var mp = await _selector.FromQueueId(request.QueueId);
            await mp.Refresh.Hold(request.QueueId);
            
            var queue = await _queueRepository.ReadById(request.QueueId);

            queue.MoveElement(queue.Elements.First(x => x.Id == request.ElementId), request.NewPosition);
            await _queueRepository.Update(queue);

            await mp.Refresh.Load(request.QueueId);
            
            return Unit.Value;
        }
    }
}