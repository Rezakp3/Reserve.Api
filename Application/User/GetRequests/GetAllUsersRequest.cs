
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.GetCommandsRequest
{
    public class GetAllUsersRequest : IRequest<StandardResult<List<Core.Entities.User>>>
    {
        public class GetAllRequestHanlder : IRequestHandler<GetAllUsersRequest, StandardResult<List<Core.Entities.User>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHanlder(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult<List<Core.Entities.User>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
            {
                var users = await _unitOfWork.User.GetAll();

                var result = new StandardResult<List<Core.Entities.User>>
                {
                    StatusCode = users is not null ? StatusCodes.Status302Found : StatusCodes.Status404NotFound,
                    Message = users is not null ? "users found" : "users not found",
                    Success = users is not null,
                    Result = users
                };

                return result;
            }
        }
    }
}
