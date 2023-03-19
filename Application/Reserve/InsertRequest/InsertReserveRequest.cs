using Core.Entities;
using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reserve.InsertRequest
{
    public class InsertReserveRequest : IRequest<Result>
    {
        public DateTime ReserveDate { get; set; }
        public Guid ReserverId { get; set; }
        public Guid LocationId { get; set; }
        public int Price { get; set; }

        public class InsertRequestHandler : IRequestHandler<InsertReserveRequest, Result>
        {
            private readonly IUnitOfWork _unitOfWork;

            public InsertRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(InsertReserveRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();

                if (!await _unitOfWork.Reserves.ValidForInsert(request.ReserveDate, request.LocationId))
                {
                    result.WithError("some one reserved this location in this date before");
                    return result;
                }

                if (!await _unitOfWork.Reserves.Insert(new Reserves
                {
                    LocationId = request.LocationId,
                    Price = request.Price,
                    ReserveDate = request.ReserveDate,
                    ReserverId = request.ReserverId
                }))
                {
                    result.WithError("oops we have a problem here.");
                    return result;
                }

                result.WithSuccess("Location Reserved.");
                return result;
            }
        }
    }
}
