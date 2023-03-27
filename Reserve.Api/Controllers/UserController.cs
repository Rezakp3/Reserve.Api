using Application.User.ChangeActivityRequest;
using Application.User.ChangePasswordRequest;
using Application.User.DeleteRequest;
using Application.User.GetCommandsRequest;
using Application.User.UpdateRequest;
using MediatR;
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
        public async Task<IActionResult> ActiveUser(Guid userId)
        {
            var changeActivityRequest = new ChangeUserActivityRequest()
            {
                Id = userId,
                Activity = true
            };

            var res = await mediator.Send(changeActivityRequest);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> DeactiveUser(Guid userId)
        {
            var changeActivityRequest = new ChangeUserActivityRequest()
            {
                Id = userId,
                Activity = true
            };

            var res = await mediator.Send(changeActivityRequest);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var res = await mediator.Send(request);
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await mediator.Send(new GetUserByIdRequest { Id = id});
            return StatusCode(res.StatusCode, res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await mediator.Send(new GetAllUsersRequest());
            return StatusCode(res.StatusCode, res);
        }
    }
}
