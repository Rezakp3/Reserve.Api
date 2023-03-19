using Core.Enums;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Reserve.UpdateRequest
{
    public class UpdateReserveRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DateTime ReserveDate { get; set; }
        public Guid LocationId { get; set; }
        public int Price { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateReserveRequest, Result>
        {

            private readonly IUnitOfWork _unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateReserveRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.Reserves.ValidForUpdate(request.Id, request.ReserveDate, request.LocationId))
                {
                    result.WithError("some one reserved this location in this date before");
                    return result;
                }

                var reserve = await _unitOfWork.Reserves.GetById(request.Id);
                if (reserve is null)
                {
                    result.WithError("reserve not found");
                    return result;
                }

                reserve.Price = request.Price;  
                reserve.ReserveDate = request.ReserveDate;
                reserve.LocationId = request.LocationId;

                if (!await _unitOfWork.Reserves.Update(reserve))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("Location information updated.");
                return result;
            }
        }
    }
}
