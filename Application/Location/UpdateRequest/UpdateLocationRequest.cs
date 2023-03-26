using Core.Dtos;
using Core.Enums;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.UpdateRequest
{
    public class UpdateLocationRequest : IRequest<StandardResult>
    {
        public Guid Id{ get; set; }
        public LocationType LocationType { get; set; }
        public string Title { get; set; }
        public string Adres { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateLocationRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();
                var location = await _unitOfWork.Locations.GetById(request.Id);
                if (location is null)
                {
                    result.Message = "Location not found";
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Success = false;
                    return result;
                }

                location.Title = request.Title;
                location.Latitude = request.Latitude;
                location.Longitude = request.Longitude;
                location.Adres = request.Adres;
                location.LocationType = request.LocationType;

                if (!await _unitOfWork.Locations.Update(location))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Location information updated.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
