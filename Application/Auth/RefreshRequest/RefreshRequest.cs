using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Auth.RefreshRequest
{
    public class RefreshRequest : IRequest<Result<Core.Entities.Auth>>
    {
        public string RefreshToken { get; set; }

        public class RefreshRequestHandler : IRequestHandler<RefreshRequest, Result<Core.Entities.Auth>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public RefreshRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Core.Entities.Auth>> Handle(RefreshRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<Core.Entities.Auth>();

                Core.Entities.Auth auth = await _unitOfWork.Auth.GetByRefreshToken(request.RefreshToken);
                var expDate = DateTime.Now.AddDays(30);
                auth.RefreshTokenExpirationDate = expDate;

                if (!await _unitOfWork.Auth.Update(auth))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("your refresh token expiration time incresed");
                result.WithValue(auth);
                return result;
            }
        }
    }
}
