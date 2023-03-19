using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.RefreshRequest
{
    public class RefreshRequest : IRequest<Result<DateTime>>
    {
        public Guid id { get; set; }

        public class RefreshRequestHandler : IRequestHandler<RefreshRequest, Result<DateTime>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public RefreshRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<DateTime>> Handle(RefreshRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<DateTime>();

                var auth = await _unitOfWork.Auth.GetById(request.id);
                var expDate = DateTime.Now.AddDays(30);
                auth.RefreshTokenExpirationDate = expDate;

                if (!await _unitOfWork.Auth.Update(auth))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("your refresh token expiration time incresed");
                result.WithValue(expDate);
                return result;
            }
        }
    }
}
