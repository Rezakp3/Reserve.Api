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
        public async Task<ActionResult<Result<Auth>>> Login(LoginRequest loginRequest)
        {
            var res = await mediator.Send(loginRequest);
            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<Result<Auth>>> Register(RegisterRequest registerRequest)
        {
            var res = await mediator.Send(registerRequest);
            return res.IsSuccess? Ok(res) : BadRequest(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Result<DateTime>>> Refresh(RefreshRequest registerRequest)
        {
            var res = await mediator.Send(registerRequest);
            return res.IsSuccess? Ok(res) : BadRequest(res);
        }
    }
}
