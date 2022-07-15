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
    [Route("debug")]
    public class DebugController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public DebugController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("init-dummy-db")]
        public async Task<Guid> InitDummyDb()
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

        [HttpGet("init-spotify-db")]
        public async Task<Guid> InitSpotifyDb()
        {
            var qId = await _mediator.Send(new CreateQueue(Platform.Spotify, "Sample MusicQueue for Spotify", "MusicQueues is up! Remember to connect your Spotify account via /queue/{id}/setup endpoint"));
            await _mediator.Send(new AddElement(qId, "spotify:track:4cOdK2wGLETKBW3PvgPWqT", "Never Gonna Give You Up"));
            await _mediator.Send(new AddElement(qId, "spotify:track:2WfaOiMkCvy7F5fcp2zZ8L", "Take on Me"));
            await _mediator.Send(new AddElement(qId, "spotify:track:5ChkMS8OtdzJeqyybCc9R5", "Billie Jean"));
            await _mediator.Send(new AddElement(qId, "spotify:track:1OOtq8tRnDM8kG2gqUPjAj", "Beat It"));
            await _mediator.Send(new AddElement(qId, "spotify:track:4ByEFOBuLXpCqvO1kw8Wdm", "I Just Died in Your Arms"));
            await _mediator.Send(new AddElement(qId, "spotify:track:2374M0fQpWi3dLnB54qaLX", "Toto Africa"));
            return qId;
        }
    }
}