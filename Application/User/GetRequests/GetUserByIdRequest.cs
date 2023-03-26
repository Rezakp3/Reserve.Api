
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.GetCommandsRequest
{
    public class GetUserByIdRequest : IRequest<StandardResult<Core.Entities.User>>
    {
        public Guid Id { get; set; }
        public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, StandardResult<Core.Entities.User>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetUserByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<Core.Entities.User>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.User.GetById(request.Id);
                var result = new StandardResult<Core.Entities.User>
                {
                    Result = user,
                    Message = user is not null ? "user found" : "user not found",
                    StatusCode = user is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound,
                    Success = user is not null
                };

                return result;
            }
        }
    }
}
