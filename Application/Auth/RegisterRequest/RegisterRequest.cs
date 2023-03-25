using Core.Entities;
using Domain.Helpers;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Auth.RegisterRequest
{
    public class RegisterRequest : IRequest<Result<Core.Entities.Auth>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<Core.Entities.Auth>>
        {
            private readonly IUnitOfWork unitOfWork;

            public RegisterRequestHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Result<Core.Entities.Auth>> Handle(RegisterRequest request, CancellationToken cancellationToken)
            {
                var user = new Core.Entities.User
                {
                    FName = request.FName,
                    LName = request.LName,
                    UserName = request.UserName,
                    PasswordHash = HashHelper.HashPassword(request.Password, ""),
                    IsActive = true
                };

                var result = new Result<Core.Entities.Auth>();

                if(!await unitOfWork.User.UserNameIsValidForInsert(request.UserName))
                {
                    result.WithError("username is taken");
                    return result;
                }

                if(!await unitOfWork.User.Insert(user))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                var auth = new Core.Entities.Auth
                {
                    Id = await unitOfWork.User.GetIdByUserName(user.UserName),
                    AccessToken = TokenHelper.GenerateAccessToken(user),
                    RefreshToken = TokenHelper.GenerateRefreshToken(),
                    RefreshTokenExpirationDate = DateTime.Now.AddDays(30)
                };

                if(!await unitOfWork.Auth.Insert(auth))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("your registration success.");
                result.WithValue(auth);
                return result;
            }
        }
    }
}
