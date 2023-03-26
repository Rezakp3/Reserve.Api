using Core.Dtos;
using Core.Entities;
using Core.Enums;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.GetRequests
{
    public class SearchLocationRequest : IRequest<StandardResult<List<Locations>>>
    {
        public string Title { get; set; }
        public int LocationType { get; set; }
        public int pageNumber { get; set; }
        public int itemCount { get; set; }

        public class SearchRequestHandler : IRequestHandler<SearchLocationRequest, StandardResult<List<Locations>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public SearchRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<List<Locations>>> Handle(SearchLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult<List<Locations>>();

                LocationSearchDto dto = new LocationSearchDto
                {
                    Title = request.Title,
                    LocationType = request.LocationType,
                    pageNumber = request.pageNumber,
                    itemCount = request.itemCount,
                };

                var locations = await _unitOfWork.Locations.SearchAndPaging(dto);

                result.Success = locations is not null;
                result.Message = locations is not null ? "locations found" : "locations not found";
                result.StatusCode = locations is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound;
                result.Result = locations;

                return result;
            }
        }
    }
}
