using Application.Authentication.LoginRequest;
using Application.Authentication.RefreshRequest;
using Application.Authentication.RegisterRequest;
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
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var res = await mediator.Send(loginRequest);
            return StatusCode(res.StatusCode , res);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var res = await mediator.Send(registerRequest);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Refresh(RefreshRequest refreshRequest)
        {
            var res = await mediator.Send(refreshRequest);
            return StatusCode(res.StatusCode, res);
        }
    }
}
