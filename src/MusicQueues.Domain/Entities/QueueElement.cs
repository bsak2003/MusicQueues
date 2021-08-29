using System;

namespace MusicQueues.Domain.Entities
{
    public class QueueElement
    {
        public QueueElement(string reference,
            string title = "Track",
            string artist = "Unknown artist",
            string album = "Unknown album",
            DateTime release = new DateTime(),
            string comment = "",
            int track = 1,
            string genre = "",
            int length = 0)
        {
            Id = Guid.NewGuid();
            Reference = reference;
            Title = title;
            Artist = artist;
            Album = album;
            Release = release;
            Comment = comment;
            Track = track;
            Genre = genre;
            Length = length;
        }
        
        public Guid Id { get; }
        public string Reference { get; }
        public string Title { get; }
        public string Artist { get; }
        public string Album { get; }
        public DateTime Release { get; }
        public string Comment { get; }
        public int Track { get; }
        public string Genre { get; }
        public int Length { get; }
    }
}