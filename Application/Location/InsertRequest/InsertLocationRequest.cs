using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;

namespace Application.Location.InsertRequest
{
    public class InsertLocationRequest : IRequest<Result>
    {
        public Locations location{ get; set; }

        public class InsertRequestHandler : IRequestHandler<InsertLocationRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public InsertRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(InsertLocationRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();
                request.location.CreateAt = DateTime.Now;
                if (!await _unitOfWork.Locations.Insert(request.location))
                {
                    result.WithError("oops we have a problem here.");
                    return result;
                }

                result.WithSuccess("Location inserted.");
                return result;
            }
        }
    }
}
