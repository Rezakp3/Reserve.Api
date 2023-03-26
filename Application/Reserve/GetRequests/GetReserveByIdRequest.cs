using Core.Dtos;
using Core.Entities;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Reserve.GetRequests
{
    public class GetReserveByIdRequest : IRequest<StandardResult<Reserves>>
    {
        public Guid Id { get; set; }

        public class GetByIdRequestHandler : IRequestHandler<GetReserveByIdRequest, StandardResult<Reserves>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<Reserves>> Handle(GetReserveByIdRequest request, CancellationToken cancellationToken)
            {
                var reserve = await _unitOfWork.Reserves.GetById(request.Id);

                var result = new StandardResult<Reserves>
                {
                    Message = reserve is not null ? "reserve found" : "reserve not found",
                    StatusCode = reserve is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound,
                    Success = reserve is not null,
                    Result = reserve
                };

                return result;
            }
        }
    }
}
