using Core.Dtos;
using Core.Entities;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.GetRequests
{
    public class GetLocationByIdRequest : IRequest<StandardResult<Locations>>
    {
        public Guid Id { get; set; }

        public class GetByIdRequestHandler : IRequestHandler<GetLocationByIdRequest, StandardResult<Locations>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<Locations>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult<Locations>();
                var location = await _unitOfWork.Locations.GetById(request.Id);

                result.Success = location is not null;
                result.Message = location is not null ? "location found" : "location not found";
                result.StatusCode = location is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound;
                result.Result = location;

                return result;
            }
        }
    }
}
