using BuildingBlocks.EventStore;
using DevTv.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevTv.Domain.Features.Videos
{
    public class UpdateVideo
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Video).NotNull();
                RuleFor(request => request.Video).SetValidator(new VideoValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public VideoDto Video { get; set; }
        }

        public class Response
        {
            public VideoDto Video { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var video = await _store.FindAsync<Video>(request.Video.VideoId);

                video.Update();

                _store.Add(video);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Video = video.ToDto()
                };
            }
        }
    }
}
