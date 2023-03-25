using Application.User.ChangeActivityRequest;
using Application.User.ChangePasswordRequest;
using Application.User.DeleteRequest;
using Application.User.GetCommandsRequest;
using Application.User.UpdateRequest;
using Core.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Reserve.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Result>> ActiveUser(Guid userId)
        {
            var changeActivityRequest = new ChangeUserActivityRequest()
            {
                Id = userId,
                Activity = true
            };

            var res = await mediator.Send(changeActivityRequest);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Result>> DeactiveUser(Guid userId)
        {
            var changeActivityRequest = new ChangeUserActivityRequest()
            {
                Id = userId,
                Activity = true
            };

            var res = await mediator.Send(changeActivityRequest);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPatch]
        public async Task<ActionResult<Result>> ChangePassword(ChangeUserPasswordRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Delete(DeleteUserRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> Update(UpdateUserRequest request)
        {
            var res = await mediator.Send(request);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<User>>> GetById(Guid id)
        {
            var res = await mediator.Send(new GetUserByIdRequest { Id = id});
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<User>>>> GetAll()
        {
            var res = await mediator.Send(new GetAllUsersRequest());
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }
    }
}
