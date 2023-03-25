using Application.Auth.GetRequests;
using Application.Auth.LoginRequest;
using Application.Auth.RefreshRequest;
using Application.Auth.RegisterRequest;
using Core.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Reserve.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<Auth>> Login(LoginRequest loginRequest)
        {
            var res = await mediator.Send(loginRequest);
            return res;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Result<Auth>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(Result<Auth>), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<Result<Auth>> Register(RegisterRequest registerRequest)
        {
            var res = await mediator.Send(registerRequest);

            return res;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Result<Auth>>> Refresh(RefreshRequest refreshRequest)
        {
            var res = await mediator.Send(refreshRequest);
            return res.IsSuccess? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<Auth>>>> GetAll()
        {
            var res = await mediator.Send(new GetAllAuthRequest());
            return res.IsSuccess ? Ok(res) : NotFound();
        }
    }
}
