using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.QueueMembers.Commands.AddMember
{
    public class AddMemberHandler : IRequestHandler<AddMember>
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly ICurrentUserService _currentUserService;

        public AddMemberHandler(IRepository<Queue> queueRepository, ICurrentUserService currentUserService)
        {
            _queueRepository = queueRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(AddMember request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            queue.AddMember(new QueueMember(_currentUserService.GetUserId().ToString(), MemberRole.User));
            return Unit.Value;
        }
    }
}