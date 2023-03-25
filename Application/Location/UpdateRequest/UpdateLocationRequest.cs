using Core.Enums;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Location.UpdateRequest
{
    public class UpdateLocationRequest : IRequest<Result>
    {
        public Guid Id{ get; set; }
        public LocationType LocationType { get; set; }
        public string Title { get; set; }
        public string Adres { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateLocationRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();
                var location = await _unitOfWork.Locations.GetById(request.Id);
                if (location is null)
                {
                    result.WithError("Location not found");
                    return result;
                }

                location.Title = request.Title;
                location.Latitude = request.Latitude;
                location.Longitude = request.Longitude;
                location.Adres = request.Adres;
                location.LocationType = request.LocationType;

                if (!await _unitOfWork.Locations.Update(location))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("Location information updated.");
                return result;
            }
        }
    }
}
