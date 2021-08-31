using System;
using MediatR;

namespace MusicQueues.Application.QueueElements.Commands.AddElement
{
    public class AddElement : IRequest<Guid>
    {
        public AddElement(string reference, string title)
        {
            Reference = reference;
            Title = title;
        }

        public Guid QueueId { get; set; }
        public string Reference { get; }
        public string Title { get; }
    }
}