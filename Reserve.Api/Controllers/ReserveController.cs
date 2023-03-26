using Application.Reserve.DeleteRequest;
using Application.Reserve.GetRequests;
using Application.Reserve.InsertRequest;
using Application.Reserve.UpdateRequest;
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
        public async Task<IActionResult> Insert(InsertReserveRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid reserveId)
        {
            var res = await mediator.Send(new ReserveDeleteRequest { reserveId = reserveId});
            return StatusCode(res.StatusCode, res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReserveRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(GetReserveByIdRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await mediator.Send(new GetAllReservesRequest());
            return StatusCode(res.StatusCode, res);
        }
    }
}
