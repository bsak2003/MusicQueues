using System;
using MediatR;

namespace MusicQueues.Application.Queues.Commands.SetupQueue;

public class SetupQueue : IRequest<Uri>
{
    public SetupQueue(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}