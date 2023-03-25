using Core.Dtos;
using Core.Entities;
using Core.Enums;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Location.GetRequests
{
    public class SearchLocationRequest : IRequest<Result<List<Locations>>>
    {
        public string Title { get; set; }
        public LocationType LocationType { get; set; }
        public int pageNumber { get; set; }
        public int itemCount { get; set; }

        public class SearchRequestHandler : IRequestHandler<SearchLocationRequest, Result<List<Locations>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public SearchRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<Locations>>> Handle(SearchLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<List<Locations>>();

                LocationSearchDto dto = new LocationSearchDto
                {
                    Title = request.Title,
                    LocationType = request.LocationType,
                    pageNumber = request.pageNumber,
                    itemCount = request.itemCount,
                };

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Locations.SearchAndPaging(dto));

                return result;
            }
        }
    }
}
