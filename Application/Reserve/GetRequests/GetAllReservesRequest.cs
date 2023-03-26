using Core.Dtos;
using Core.Entities;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Reserve.GetRequests
{
    public class GetAllReservesRequest : IRequest<StandardResult<List<Reserves>>>
    {
        public class GetAllRequestHandler : IRequestHandler<GetAllReservesRequest, StandardResult<List<Reserves>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<List<Reserves>>> Handle(GetAllReservesRequest request, CancellationToken cancellationToken)
            {
                var reserves = await _unitOfWork.Reserves.GetAll();

                var result = new StandardResult<List<Reserves>>
                {
                    Message = reserves is not null ? "Reserves found" : "Reserves not found",
                    StatusCode = reserves is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound,
                    Success = reserves is not null,
                    Result = reserves
                };

                return result;
            }
        }
    }
}
