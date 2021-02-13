using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevTv.Domain.Features.Videos;
using System.Net;
using System.Threading.Tasks;

namespace DevTv.Api.Controllers
{
    [ApiController]
    [Route("api/videos")]
    public class VideosController
    {
        private readonly IMediator _mediator;

        public VideosController(IMediator mediator) => _mediator = mediator;

        [HttpPost(Name = "CreateVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateVideo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateVideo.Response>> Create([FromBody]CreateVideo.Request request)
            => await _mediator.Send(request);

        [HttpPut(Name = "UpdateVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateVideo.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateVideo.Response>> Update([FromBody]UpdateVideo.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{videoId}", Name = "RemoveVideoRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute]RemoveVideo.Request request)
            => await _mediator.Send(request);

        [HttpGet("{videoId}", Name = "GetVideoByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideoById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetVideoById.Response>> GetById([FromRoute]GetVideoById.Request request)
        {
            var response = await _mediator.Send(request);

            if (response.Video == null)
            {
                return new NotFoundObjectResult(request.VideoId);
            }

            return response;
        }

        [HttpGet(Name = "GetVideosRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVideos.Response>> Get()
            => await _mediator.Send(new GetVideos.Request());           
    }
}
