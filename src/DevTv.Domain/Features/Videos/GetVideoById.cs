using BuildingBlocks.Abstractions;
using DevTv.Core.Data;
using DevTv.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevTv.Domain.Features.Videos
{
    public class GetVideoById
    {
        public class Request : IRequest<Response> {  
            public Guid VideoId { get; set; }        
        }

        public class Response
        {
            public VideoDto Video { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDevTvDbContext _context;

            public Handler(IDevTvDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var video = await _context.Videos.FindAsync(request.VideoId);

                return new Response() { 
                    Video = video.ToDto()
                };
            }
        }
    }
}
