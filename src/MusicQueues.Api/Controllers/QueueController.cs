﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicQueues.Api.Models;
using MusicQueues.Application.QueueElements.Commands.AddElement;
using MusicQueues.Application.QueueElements.Commands.DeleteElement;
using MusicQueues.Application.QueueMembers.Commands.AddMember;
using MusicQueues.Application.QueueMembers.Commands.DeleteMember;
using MusicQueues.Application.Queues.Commands.CreateQueue;
using MusicQueues.Application.Queues.Commands.DeleteQueue;
using MusicQueues.Application.Queues.Queries.ReadQueueById;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

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
            return await _mediator.Send(new CreateQueue(Platform.Dummy));
        }

        [HttpDelete("{id:guid}")]
        public async Task DeleteQueue(Guid id)
        {
            await _mediator.Send(new DeleteQueue(id));
        }

        [HttpGet("{id:guid}/join")]
        public async Task JoinQueue(Guid id)
        {
            await _mediator.Send(new AddMember(id));
        }

        [HttpDelete("{queueId:guid}/kick/{memberId:guid}")]
        public async Task KickFromQueue(Guid queueId, Guid memberId)
        {
            await _mediator.Send(new DeleteMember(queueId, memberId));
        }

        [HttpPost("{queueId:guid}/addSong")]
        public async Task AddSong(Guid queueId, SongModel model)
        {
            await _mediator.Send(new AddElement(model.Reference, model.Title));
        }

        [HttpDelete("{queueId:guid}/removeSong/{songId:guid}")]
        public async Task RemoveSong(Guid queueId, Guid songId)
        {
            await _mediator.Send(new DeleteElement(queueId, songId));
        }
    }
}