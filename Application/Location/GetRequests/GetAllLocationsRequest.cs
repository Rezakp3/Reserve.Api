using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Location.GetRequests
{
    public class GetAllLocationsRequest : IRequest<Result<List<Locations>>>
    {
        public class GetAllRequestHandler : IRequestHandler<GetAllLocationsRequest, Result<List<Locations>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<Locations>>> Handle(GetAllLocationsRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<List<Locations>>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Locations.GetAll());

                return result;
            }
        }
    }
}
