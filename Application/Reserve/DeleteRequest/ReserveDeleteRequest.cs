
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Reserve.DeleteRequest
{
    public class ReserveDeleteRequest : IRequest<StandardResult>
    {
        public Guid reserveId { get; set; }

        public class DeleteRequestHandler : IRequestHandler<ReserveDeleteRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(ReserveDeleteRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.Reserves.Delete(request.reserveId))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Reserve deleted .";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;

                return result;
            }
        }
    }
}
