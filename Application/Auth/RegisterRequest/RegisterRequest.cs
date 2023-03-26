using Core.Dtos;
using Core.Entities;
using Domain.Helpers;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Authentication.RegisterRequest
{
    public class RegisterRequest : IRequest<StandardResult<AuthDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public class RegisterRequestHandler : IRequestHandler<RegisterRequest, StandardResult<AuthDto>>
        {
            private readonly IUnitOfWork unitOfWork;

            public RegisterRequestHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<AuthDto>> Handle(RegisterRequest request, CancellationToken cancellationToken)
            {

                var result = new StandardResult<AuthDto>();

                if(!await unitOfWork.User.UserNameIsValidForInsert(request.UserName))
                {
                    result.Message = "username is taken";
                    result.StatusCode = StatusCodes.Status406NotAcceptable;
                    result.Success = false;
                    return result;
                }

                var user = new Core.Entities.User
                {
                    Id = Guid.NewGuid(),
                    FName = request.FName,
                    LName = request.LName,
                    UserName = request.UserName,
                    PasswordHash = HashHelper.HashPassword(request.Password, ""),
                    IsActive = true,
                    RefreshToken = TokenHelper.GenerateRefreshToken(),
                    RefreshTokenExpirationDate = DateTime.Now.AddDays(1)                
                };

                user.AccessToken = TokenHelper.GenerateAccessToken(user);
                

                if(!await unitOfWork.User.Insert(user))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                var auth = new AuthDto
                {
                    Id = user.Id,
                    AccessToken = user.AccessToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpirationDate = user.RefreshTokenExpirationDate
                };

                result.Message = "your registration success.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                result.Result = auth;
                return result;
            }
        }
    }
}
