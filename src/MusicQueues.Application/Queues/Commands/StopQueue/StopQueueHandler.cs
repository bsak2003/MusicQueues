using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Commands.StopQueue
{
    public class StopQueueHandler : IRequestHandler<StopQueue>
    {
        private readonly IRepository<Queue> _repository;
        private readonly IEnumerable<IMediaPlayer> _mediaPlayers;

        public StopQueueHandler(IRepository<Queue> repository, IEnumerable<IMediaPlayer> mediaPlayers)
        {
            _repository = repository;
            _mediaPlayers = mediaPlayers;
        }

        public async Task<Unit> Handle(StopQueue request, CancellationToken cancellationToken)
        {
            var queue = await _repository.ReadById(request.QueueId);
            var mp = _mediaPlayers.First(x => x.Platform == queue.Platform);
            mp.StopPlayback(queue.Id);
            return Unit.Value;
        }
    }
}