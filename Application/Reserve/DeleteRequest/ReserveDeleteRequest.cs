using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Reserve.DeleteRequest
{
    public class ReserveDeleteRequest : IRequest<Result>
    {
        public Guid reserveId { get; set; }

        public class DeleteRequestHandler : IRequestHandler<ReserveDeleteRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public DeleteRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(ReserveDeleteRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.Reserves.Delete(request.reserveId))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("Reserve deleted .");
                return result;
            }
        }
    }
}
