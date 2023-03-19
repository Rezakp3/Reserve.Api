using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Location.DeleteRequest
{
    public class LocationDeleteRequest : IRequest<Result>
    {
        public Guid Id { get; set; }

        public class DeleteRequestHandler : IRequestHandler<LocationDeleteRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(LocationDeleteRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.Locations.Delete(request.Id))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("Location deleted .");
                return result;
            }
        }
    }
}
