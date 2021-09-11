using MusicQueues.Domain.Enums;

namespace MusicQueues.Api.Models
{
    public class ElevateMemberModel
    {
        public MemberRole ElevateTo { get; set; }
    }
}