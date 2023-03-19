using Domain.Helpers;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.User.ChangePasswordRequest
{
    public class ChangeUserPasswordRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }

        public class ChangePasswordRequestHandler : IRequestHandler<ChangeUserPasswordRequest, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ChangePasswordRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.User.ChangePassword(request.Id , HashHelper.HashPassword(request.Password, "")))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("user password updated.");
                return result;
            }
        }
    }
}
