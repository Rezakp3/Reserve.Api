using MediatR;
using FluentResults;
using Infrastructure.UnitOfWork;
using Domain.Helpers;

namespace Application.Auth.LoginRequest
{
    public class LoginRequest : IRequest<Result<Core.Entities.Auth>>
    {
        public string userName { get; set; }
        public string password { get; set; }

        public class LoginRequestHandler : IRequestHandler<LoginRequest, Result<Core.Entities.Auth>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public LoginRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Core.Entities.Auth>> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<Core.Entities.Auth>();
                var user = await _unitOfWork.User.GetUserByUserName(request.userName);
                if (user is null)
                {
                    result.WithError("username not found.");
                    return result;
                }

                var passwordHash = HashHelper.HashPassword(request.password, "");

                if (user.PasswordHash != passwordHash)
                {
                    result.WithError("username or password is incorrect.");
                    return result;
                }

                var auth = new Core.Entities.Auth();

                auth.Id = user.Id;
                auth.AccessToken = TokenHelper.GenerateAccessToken(user);
                auth.RefreshToken = TokenHelper.GenerateRefreshToken();
                auth.RefreshTokenExpirationDate = DateTime.Now.AddDays(30);

                if (await _unitOfWork.Auth.GetById(user.Id) is not null)
                {
                    if (!await _unitOfWork.Auth.Update(auth))
                    {
                        result.WithError("oops we have a problem here.");
                        return result;
                    }
                }
                else
                {
                    if (!await _unitOfWork.Auth.Insert(auth))
                    {
                        result.WithError("oops we have a problem here.");
                        return result;
                    }
                }

                result.WithSuccess("Login successfull ...");
                result.WithValue(auth);
                return result;
            }

        }

    }
}
