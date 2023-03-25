using Core.Entities;
using Core.Enums;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Location.InsertRequest
{
    public class InsertLocationRequest : IRequest<Result>
    {
        public LocationType LocationType { get; set; }
        public string Title { get; set; }
        public string Adres { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public class InsertRequestHandler : IRequestHandler<InsertLocationRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public InsertRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(InsertLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();
                Locations loc = new Locations()
                {
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
                    result.WithError("oops we have a problem here.");
                    return result;
                }

                result.WithSuccess("Location inserted.");
                return result;
            }
        }
    }
}
