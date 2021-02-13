using System;

namespace DevTv.Domain.Features.Videos
{
    public class VideoDto
    {
        public Guid VideoId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
