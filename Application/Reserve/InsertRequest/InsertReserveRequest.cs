using Core.Dtos;
using Core.Entities;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reserve.InsertRequest
{
    public class InsertReserveRequest : IRequest<StandardResult>
    {
        public DateTime ReserveDate { get; set; }
        public Guid ReserverId { get; set; }
        public Guid LocationId { get; set; }
        public int Price { get; set; }

        public class InsertRequestHandler : IRequestHandler<InsertReserveRequest, StandardResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public InsertRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(InsertReserveRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.Reserves.ValidForInsert(request.ReserveDate, request.LocationId))
                {
                    result.Message = "some one reserved this location in this date before";
                    result.StatusCode = StatusCodes.Status406NotAcceptable;
                    result.Success = false;
                    return result;
                }

                if (!await _unitOfWork.Reserves.Insert(new Reserves
                {
                    Id = Guid.NewGuid(),
                    LocationId = request.LocationId,
                    Price = request.Price,
                    ReserveDate = request.ReserveDate,
                    ReserverId = request.ReserverId
                }))
                {
                    result.Message = "oops we have a problem here.";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Location Reserved.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;

                return result;
            }
        }
    }
}
