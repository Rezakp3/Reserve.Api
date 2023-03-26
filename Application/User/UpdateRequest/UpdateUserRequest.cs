
using Core.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UpdateRequest
{
    public class UpdateUserRequest : IRequest<StandardResult>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateUserRequest, StandardResult>
        {
            private readonly IUnitOfWork unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<StandardResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
            {
                var result = new StandardResult();
                var user = await unitOfWork.User.GetById(request.Id);
                if(user is null)
                {
                    result.Message = "User not found";
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Success = false;
                    return result;
                }

                if(!await unitOfWork.User.UserNameIsValidForUpdate(request.Id , request.UserName))
                {
                    result.Message = "UserName is not valid for Update";
                    result.StatusCode = StatusCodes.Status406NotAcceptable;
                    result.Success = false;
                    return result;
                }

                user.UserName = request.UserName;
                user.FName = request.FName;
                user.LName = request.LName;

                if(!await unitOfWork.User.Update(user))
                {
                    result.Message = "oops we have a problem here";
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    result.Success = false;
                    return result;
                }

                result.Message = "user information updated.";
                result.StatusCode = StatusCodes.Status200OK;
                result.Success = true;
                return result;
            }
        }
    }
}
