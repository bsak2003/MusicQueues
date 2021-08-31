using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueMembers.Commands.DeleteMember
{
    public class DeleteMemberHandler : IRequestHandler<DeleteMember>
    {
        private readonly IRepository<Queue> _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMemberHandler(IRepository<Queue> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteMember request, CancellationToken cancellationToken)
        {
            var queue = await _repository.ReadById(request.QueueId);
            queue.RemoveMember(queue.Members.First(x => x.Id == request.MemberId));
            await _repository.Update(queue);
            return Unit.Value;
        }
    }
}