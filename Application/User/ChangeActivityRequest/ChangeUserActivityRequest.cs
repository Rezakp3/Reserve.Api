
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.ChangeActivityRequest
{
    public class ChangeUserActivityRequest : IRequest<StandardResult>
    {
        public Guid Id { get; set; }
        public bool Activity { get; set; }

        public class ChangeActivityRequestHandler : IRequestHandler<ChangeUserActivityRequest , StandardResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ChangeActivityRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(ChangeUserActivityRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.User.ChangeActivity(request.Id,request.Activity))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "user activity updated.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
