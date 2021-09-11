using System;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Queries.Common
{
    public class QueueElementDto
    {
        public QueueElementDto(QueueElement element, int index)
        {
            Position = index;
            Id = element.Id;
            ElementCreatorId = element.ElementCreatorId;
            Reference = element.Reference;
            Title = element.Title;
            Artist = element.Artist;
            Album = element.Album;
            Release = element.Release;
            Comment = element.Comment;
            Track = element.Track;
            Genre = element.Genre;
            Length = element.Length;
            CoverUrl = element.CoverUrl;
        }
        
        public int Position { get; }
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