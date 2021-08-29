using System;
using System.Collections.Generic;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class Queue
    {
        private readonly List<QueueElement> _elements = new List<QueueElement>();
        private readonly List<QueueMember> _members = new List<QueueMember>();
        
        public Queue(Platform platform)
        {
            Id = new Guid();
            Platform = platform;
        }
        
        public readonly Guid Id;
        public readonly Platform Platform;

        public IReadOnlyCollection<QueueElement> Elements => _elements;
        public IReadOnlyCollection<QueueMember> Members => _members;
        
        public void AddElement(QueueElement element)
        {
            _elements.Add(element);
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