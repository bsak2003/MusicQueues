using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Infrastructure.Persistence.DummyQueueRepository
{
    public class DummyQueueRepository : IRepository<Queue>
    {
        private readonly List<Queue> _repository = new List<Queue>();
        public Task<Queue> ReadById(Guid id)
        {
            return Task.FromResult(_repository.Find(x => x.Id == id));
        }

        public Task<IEnumerable<Queue>> ReadAll()
        {
            return Task.FromResult(_repository.AsEnumerable());
        }

        public Task Create(Queue entity)
        {
            _repository.Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(Queue entity)
        {
            _repository[_repository.IndexOf(entity)] = entity;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            _repository.Remove(_repository.Find(x => x.Id == id));
            return Task.CompletedTask;
        }
    }
}