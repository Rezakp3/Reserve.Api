using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Location.GetRequests
{
    public class GetLocationByIdRequest : IRequest<Result<Locations>>
    {
        public Guid Id { get; set; }

        public class GetByIdRequestHandler : IRequestHandler<GetLocationByIdRequest, Result<Locations>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Locations>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<Locations>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.Locations.GetById(request.Id));

                return result;
            }
        }
    }
}
