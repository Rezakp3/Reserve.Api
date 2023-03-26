using Core.Dtos;
using Core.Entities;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.GetRequests
{
    public class GetAllLocationsRequest : IRequest<StandardResult<List<Locations>>>
    {
        public class GetAllRequestHandler : IRequestHandler<GetAllLocationsRequest, StandardResult<List<Locations>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<List<Locations>>> Handle(GetAllLocationsRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult<List<Locations>>();

                var locations = await _unitOfWork.Locations.GetAll();
                result.Success = locations is not null;
                result.Message = locations is not null ? "Locations found" : "locations not found";
                result.StatusCode = locations is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound;
                result.Result = locations;

                return result;
            }
        }
    }
}
