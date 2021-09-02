using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.QueueMembers.Commands.UpdateMember
{
    public class UpdateMemberHandler : IRequestHandler<UpdateMember>
    {
        private readonly IRepository<Queue> _queueRepository;

        public UpdateMemberHandler(IRepository<Queue> queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<Unit> Handle(UpdateMember request, CancellationToken cancellationToken)
        {
            var queue = await _queueRepository.ReadById(request.QueueId);
            queue.Members.First(x => x.Id == request.MemberId).UpdateRole(request.NewRole);
            await _queueRepository.Update(queue);
            return Unit.Value;
        }
    }
}