
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Location.DeleteRequest
{
    public class LocationDeleteRequest : IRequest<StandardResult>
    {
        public Guid id { get; set; }

        public class DeleteRequestHandler : IRequestHandler<LocationDeleteRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(LocationDeleteRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.Locations.Delete(request.id))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Location deleted .";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
