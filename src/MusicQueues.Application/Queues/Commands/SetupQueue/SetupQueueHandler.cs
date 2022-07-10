using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Application.Queues.Commands.SetupQueue;

public class SetupQueueHandler : IRequestHandler<SetupQueue, Uri>
{
    private readonly IMediaPlayerSelector _selector;

    public SetupQueueHandler(IMediaPlayerSelector selector)
    {
        _selector = selector;
    }

    public async Task<Uri> Handle(SetupQueue request, CancellationToken cancellationToken)
    {
        var mp = await _selector.FromQueueId(request.Id);
        return await mp.Player.Setup(request.Id);
    }
}