using Application.Reserve.DeleteRequest;
using Application.Reserve.GetRequests;
using Application.Reserve.InsertRequest;
using Application.Reserve.UpdateRequest;
using Core.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Reserve.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReserveController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReserveController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Insert(InsertReserveRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Delete(ReserveDeleteRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> Update(UpdateReserveRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<Locations>>> GetById(GetReserveByIdRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<Locations>>>> GetAll()
        {
            var res = await mediator.Send(new GetAllReservesRequest());
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }
    }
}
