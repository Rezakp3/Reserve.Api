using MediatR;
using Infrastructure.UnitOfWork;
using Domain.Helpers;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Authentication.LoginRequest
{
    public class LoginRequest : IRequest<StandardResult<AuthDto>>
    {
        public string userName { get; set; }
        public string password { get; set; }

        public class LoginRequestHandler : IRequestHandler<LoginRequest, StandardResult<AuthDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public LoginRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<AuthDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult<AuthDto>();
                var user = await _unitOfWork.User.GetUserByUserName(request.userName);
                if (user is null)
                {
                    result.Message = "username not found.";
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Success = false;
                    return result;
                }

                var passwordHash = HashHelper.HashPassword(request.password, "");

                if (user.PasswordHash != passwordHash)
                {
                    result.Message = "username or password is incorrect.";
                    result.StatusCode = StatusCodes.Status401Unauthorized;
                    result.Success = false;
                    return result;
                }

                user.AccessToken = TokenHelper.GenerateAccessToken(user);
                user.RefreshToken = TokenHelper.GenerateRefreshToken();
                user.RefreshTokenExpirationDate = DateTime.Now.AddMonths(1).Date;

                if(!await _unitOfWork.User.Update(user))
                {
                    result.Message = "oops we have a problem here";
                    result.Success = false;
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    return result;
                }

                var auth = new AuthDto
                {
                    Id = user.Id,
                    AccessToken = user.AccessToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpirationDate = user.RefreshTokenExpirationDate
                };


                result.Message = "Login successfull ...";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                result.Result = auth;
                return result;
            }

        }

    }
}
