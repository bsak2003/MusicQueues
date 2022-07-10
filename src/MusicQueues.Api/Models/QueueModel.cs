using MusicQueues.Domain.Enums;

namespace MusicQueues.Api.Models
{
    public class QueueModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Platform Platform { get; set; }
    }
}