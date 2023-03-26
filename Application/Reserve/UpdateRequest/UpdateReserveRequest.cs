using Core.Dtos;
using Core.Enums;

using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Reserve.UpdateRequest
{
    public class UpdateReserveRequest : IRequest<StandardResult>
    {
        public Guid Id { get; set; }
        public DateTime ReserveDate { get; set; }
        public Guid LocationId { get; set; }
        public int Price { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateReserveRequest, StandardResult>
        {

            private readonly IUnitOfWork _unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(UpdateReserveRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();

                if (!await _unitOfWork.Reserves.ValidForUpdate(request.Id, request.ReserveDate, request.LocationId))
                {
                    result.Message = "some one reserved this location in this date before";
                    result.StatusCode = StatusCodes.Status406NotAcceptable;
                    result.Success = false;
                    return result;
                }

                var reserve = await _unitOfWork.Reserves.GetById(request.Id);
                if (reserve is null)
                {
                    result.Message = "reserve not found";
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Success = false;
                    return result;
                }

                reserve.Price = request.Price;  
                reserve.ReserveDate = request.ReserveDate;
                reserve.LocationId = request.LocationId;

                if (!await _unitOfWork.Reserves.Update(reserve))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "Location information updated.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true; 
                return result;
            }
        }
    }
}
