using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.User.GetCommandsRequest
{
    public class GetAllUsersRequest : IRequest<Result<List<Core.Entities.User>>>
    {
        public class GetAllRequestHanlder : IRequestHandler<GetAllUsersRequest, Result<List<Core.Entities.User>>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetAllRequestHanlder(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<List<Core.Entities.User>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<List<Core.Entities.User>>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.User.GetAll());

                return result;
            }
        }
    }
}
