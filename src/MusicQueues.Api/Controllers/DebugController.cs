using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicQueues.Application.QueueElements.Commands.AddElement;
using MusicQueues.Application.Queues.Commands.CreateQueue;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Api.Controllers
{
    [ApiController]
    [Route("dbg")]
    public class DebugController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public DebugController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("init-db")]
        public async Task<Guid> InitDb()
        {
            var qId = await _mediator.Send(new CreateQueue(Platform.Dummy, "Sample MusicQueue", "MusicQueues is up!"));
            await _mediator.Send(new AddElement(qId, "rickroll-01", "Never Gonna Give You Up"));
            await _mediator.Send(new AddElement(qId, "allout80s/1", "Take on Me"));
            await _mediator.Send(new AddElement(qId, "allout80s/2", "Billie Jean"));
            await _mediator.Send(new AddElement(qId, "allout80s/3", "Beat It"));
            await _mediator.Send(new AddElement(qId, "allout80s/4", "I Just Died in Your Arms"));
            await _mediator.Send(new AddElement(qId, "allout80s/5", "Toto Africa"));
            return qId;
        } 
    }
}