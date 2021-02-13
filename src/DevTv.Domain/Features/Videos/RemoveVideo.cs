using DevTv.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using BuildingBlocks.EventStore;

namespace DevTv.Domain.Features.Videos
{
    public class RemoveVideo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid VideoId { get; set; }
        }

        public class Response
        {
            public VideoDto Video { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _context;

            public Handler(IEventStore context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var video = await _context.FindAsync<Video>(request.VideoId);

                video.Remove();

                _context.Add(video);

                await _context.SaveChangesAsync(cancellationToken);

                return new () { };
            }
        }
    }
}
