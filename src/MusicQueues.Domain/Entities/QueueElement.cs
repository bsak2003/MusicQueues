using System;

namespace MusicQueues.Domain.Entities
{
    public class QueueElement
    {
        public QueueElement(Guid elementCreatorId,
            string reference,
            string title = "Track",
            string artist = "Unknown artist",
            string album = "Unknown album",
            DateTime release = new DateTime(),
            string comment = "",
            int track = 1,
            string genre = "",
            int length = 0,
            string coverUrl = ""
            )
        {
            Id = Guid.NewGuid();
            ElementCreatorId = elementCreatorId;
            Reference = reference;
            Title = title;
            Artist = artist;
            Album = album;
            Release = release;
            Comment = comment;
            Track = track;
            Genre = genre;
            Length = length;
            CoverUrl = coverUrl;
        }
        
        public Guid Id { get; }
        public Guid ElementCreatorId { get; }
        public string Reference { get; }
        public string Title { get; }
        public string Artist { get; }
        public string Album { get; }
        public DateTime Release { get; }
        public string Comment { get; }
        public int Track { get; }
        public string Genre { get; }
        public int Length { get; }
        public string CoverUrl { get; }
    }
}