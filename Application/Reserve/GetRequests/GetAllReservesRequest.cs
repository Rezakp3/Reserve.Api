using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Reserve.GetRequests
{
    public class GetAllReservesRequest : IRequest<Result<List<Reserves>>>
    {
        public class GetAllRequestHandler : IRequestHandler<GetAllReservesRequest, Result<List<Reserves>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<Reserves>>> Handle(GetAllReservesRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<List<Reserves>>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Reserves.GetAll());

                return result;
            }
        }
    }
}
