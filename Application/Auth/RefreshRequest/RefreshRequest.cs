using Core.Dtos;
using Core.Entities;
using Domain.Helpers;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Authentication.RefreshRequest
{
    public class RefreshRequest : IRequest<StandardResult<AuthDto>>
    {
        public string RefreshToken { get; set; }

        public class RefreshRequestHandler : IRequestHandler<RefreshRequest, StandardResult<AuthDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public RefreshRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<AuthDto>> Handle(RefreshRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult<AuthDto>();

                var user = await _unitOfWork.User.GetUserByRefreshToken(request.RefreshToken);
                user.RefreshTokenExpirationDate = DateTime.Now.AddMonths(1).Date;
                user.RefreshToken = TokenHelper.GenerateRefreshToken();

                if (!await _unitOfWork.User.Update(user))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "your refresh token expiration time incresed";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                result.Result = new AuthDto
                {
                    Id = user.Id,
                    AccessToken = user.AccessToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpirationDate = user.RefreshTokenExpirationDate
                };

                return result;
            }
        }
    }
}
