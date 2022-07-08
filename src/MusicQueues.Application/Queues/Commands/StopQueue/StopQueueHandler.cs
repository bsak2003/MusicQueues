using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.StopQueue
{
    public class StopQueueHandler : IRequestHandler<StopQueue>
    {
        private readonly IRepository<Queue> _repository;
        private readonly IMediaPlayerSelector _selector;

        public StopQueueHandler(IRepository<Queue> repository, IMediaPlayerSelector selector)
        {
            _repository = repository;
            _selector = selector;
        }

        public async Task<Unit> Handle(StopQueue request, CancellationToken cancellationToken)
        {
            var queue = await _repository.ReadById(request.QueueId);

            queue.UpdateStatus(Status.Stopped);
            
            await _selector.FromQueue(queue).Playback.Stop(queue.Id);
            await _repository.Update(queue);
            
            return Unit.Value;
        }
    }
}