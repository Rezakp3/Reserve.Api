using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Auth.GetRequests
{
    public class GetAllAuthRequest : IRequest<Result<List<Core.Entities.Auth>>>
    {
        public class GetAllRequestHandler : IRequestHandler<GetAllAuthRequest, Result<List<Core.Entities.Auth>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<Core.Entities.Auth>>> Handle(GetAllAuthRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<List<Core.Entities.Auth>>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Auth.GetAll());

                return result;
            }
        }
    }
}
