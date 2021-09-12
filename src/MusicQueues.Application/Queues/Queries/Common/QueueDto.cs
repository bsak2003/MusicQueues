using System;
using System.Collections.Generic;
using System.Linq;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Queries.Common
{
    public class QueueDto
    {
        public QueueDto(Queue queue)
        {
            Id = queue.Id;
            Platform = queue.Platform;
            Title = queue.Title;
            Description = queue.Description;
            Status = queue.Status;
            Elements = new List<QueueElementDto>();
            Members = new List<QueueMemberDto>();

            foreach (var element in queue.Elements)
            {
                Elements.Add(new QueueElementDto(element, queue.Elements.IndexOf(element)));
            }

            foreach (var member in queue.Members)
            {
                Members.Add(new QueueMemberDto(member));
            }
        }

        public Guid Id { get; }
        public Platform Platform { get; }
        public string Title { get; }
        public string Description { get; }
        public Status Status { get; }
        public List<QueueElementDto> Elements { get; }
        public List<QueueMemberDto> Members { get; }
    }
}