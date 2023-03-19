using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Reserve.GetRequests
{
    public class GetReserveByIdRequest : IRequest<Result<Reserves>>
    {
        public Guid Id { get; set; }

        public class GetByIdRequestHandler : IRequestHandler<GetReserveByIdRequest, Result<Reserves>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Reserves>> Handle(GetReserveByIdRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<Reserves>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Reserves.GetById(request.Id));

                return result;
            }
        }
    }
}
