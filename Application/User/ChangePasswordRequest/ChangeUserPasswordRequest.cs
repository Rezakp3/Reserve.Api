using Core.Dtos;
using Domain.Helpers;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.ChangePasswordRequest
{
    public class ChangeUserPasswordRequest : IRequest<StandardResult>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }

        public class ChangePasswordRequestHandler : IRequestHandler<ChangeUserPasswordRequest, StandardResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ChangePasswordRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.User.ChangePassword(request.Id , HashHelper.HashPassword(request.Password, "")))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "user password updated.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
