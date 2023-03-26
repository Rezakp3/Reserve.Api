using Core.Dtos;
using Core.Entities;
using Core.Enums;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.InsertRequest
{
    public class InsertLocationRequest : IRequest<StandardResult>
    {
        public LocationType LocationType { get; set; }
        public string Title { get; set; }
        public string Adres { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public class InsertRequestHandler : IRequestHandler<InsertLocationRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public InsertRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(InsertLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();
                Locations loc = new Locations()
                {
                    Id = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    LocationType = request.LocationType,
                    Title = request.Title,
                    Adres = request.Adres,
                    CreatorId = request.CreatorId,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                };

                if (!await _unitOfWork.Locations.Insert(loc))
                {
                    result.Message = "oops we have a problem here.";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Location inserted.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
