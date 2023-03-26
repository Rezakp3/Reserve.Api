using Application.Location.DeleteRequest;
using Application.Location.GetRequests;
using Application.Location.InsertRequest;
using Application.Location.UpdateRequest;
using Core.Entities;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Reserve.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "JustActive")]
    public class LocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public LocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertLocationRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await mediator.Send(new LocationDeleteRequest { id = id});
            return StatusCode(res.StatusCode, res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await mediator.Send(new GetLocationByIdRequest { Id = id});
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await mediator.Send(new GetAllLocationsRequest());
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAndPagination(SearchLocationRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }
    }
}
