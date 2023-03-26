

using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.DeleteRequest
{
    public class DeleteUserRequest : IRequest<StandardResult>
    {
        public Guid Id { get; set; }

        public class DeleteRequestHandler : IRequestHandler<DeleteUserRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.User.Delete(request.Id))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
