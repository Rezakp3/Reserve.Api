using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.User.GetCommandsRequest
{
    public class GetUserByIdRequest : IRequest<Result<Core.Entities.User>>
    {
        public Guid Id { get; set; }
        public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<Core.Entities.User>>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetUserByIdRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Core.Entities.User>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
            {
                var result = new Result<Core.Entities.User>();

                result.WithSuccess("");
                result.WithValue(await _unitOfWork.User.GetById(request.Id));

                return result;
            }
        }
    }
}
