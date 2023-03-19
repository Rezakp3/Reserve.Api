using Core.Dtos;
using Core.Entities;
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
        public LocationSearchDto dto{ get; set; }

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

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Locations.SearchAndPaging(request.dto));

                return result;
            }
        }
    }
}
