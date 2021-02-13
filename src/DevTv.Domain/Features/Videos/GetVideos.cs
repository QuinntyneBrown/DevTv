using BuildingBlocks.Abstractions;
using DevTv.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevTv.Domain.Features.Videos
{
    public class GetVideos
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<VideoDto> Videos { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Videos = _context.Set<Video>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
