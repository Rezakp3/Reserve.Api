
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.User.DeleteRequest
{
    public class DeleteUserRequest : IRequest<Result>
    {
        public Guid Id { get; set; }

        public class DeleteRequestHandler : IRequestHandler<DeleteUserRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.User.Delete(request.Id))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("user deleted .");
                return result;
            }
        }
    }
}
