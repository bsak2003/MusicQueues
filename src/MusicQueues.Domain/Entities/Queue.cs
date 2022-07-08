using System;
using System.Collections.Generic;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class Queue
    {
        private readonly List<QueueElement> _elements = new();
        private readonly List<QueueMember> _members = new();
        
        public Queue(Platform platform, string title = "", string description = "")
        {
            Id = Guid.NewGuid();
            Platform = platform;
            Title = title;
            Description = description;
            Status = Status.Created;
        }
        
        public Guid Id { get; }
        public Platform Platform { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public IList<QueueElement> Elements => _elements;
        public IEnumerable<QueueMember> Members => _members;

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
        }
        
        public void AddElement(QueueElement element, int index = -1)
        {
            if (index == -1)
            {
                index = _elements.Count;
            }

            _elements.Insert(index, element);
        }

        public void MoveElement(QueueElement element, int index)
        {
            RemoveElement(element);
            AddElement(element, index);
        }

        public void RemoveElement(QueueElement element)
        {
            _elements.Remove(element);
        }
        
        public void AddMember(QueueMember member)
        {
            _members.Add(member);
        }

        public void RemoveMember(QueueMember member)
        {
            _members.Remove(member);
        }
    }
}