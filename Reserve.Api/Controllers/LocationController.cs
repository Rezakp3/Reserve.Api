using Application.Location.DeleteRequest;
using Application.Location.GetRequests;
using Application.Location.InsertRequest;
using Application.Location.UpdateRequest;
using Core.Entities;
using FluentResults;
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
        public async Task<ActionResult<Result>> Insert(InsertLocationRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Delete(LocationDeleteRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Update(UpdateLocationRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<Locations>>> GetById(Guid id)
        {

            var res = await mediator.Send(new GetLocationByIdRequest { Id = id});
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<Locations>>>> GetAll()
        {
            var res = await mediator.Send(new GetAllLocationsRequest());
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result<List<Locations>>>> SearchAndPagination(SearchLocationRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }
    }
}
