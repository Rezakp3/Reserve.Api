using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.User.ChangeActivityRequest
{
    public class ChangeUserActivityRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
        public bool Activity { get; set; }

        public class ChangeActivityRequestHandler : IRequestHandler<ChangeUserActivityRequest , Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ChangeActivityRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(ChangeUserActivityRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.User.ChangeActivity(request.Id,request.Activity))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("user activity updated.");
                return result;
            }
        }
    }
}
