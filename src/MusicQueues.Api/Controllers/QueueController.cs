using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicQueues.Application.Queues.Commands.CreateQueue;
using MusicQueues.Application.Queues.Commands.DeleteQueue;
using MusicQueues.Application.Queues.Queries.ReadQueueById;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Api.Controllers
{
    [ApiController]
    [Route("queue")]
    public class QueueController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public QueueController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<Queue> GetQueueById(Guid id)
        {
            return await _mediator.Send(new ReadQueueById(id));
        }

        [HttpPost]
        public async Task<Guid> CreateQueue()
        {
            return await _mediator.Send(new CreateQueue());
        }

        [HttpDelete("{id:guid}")]
        public async Task DeleteQueue(Guid id)
        {
            await _mediator.Send(new DeleteQueue(id));
        }
    }
}