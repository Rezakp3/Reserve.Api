using FluentResults;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UpdateRequest
{
    public class UpdateUserRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public class UpdateRequestHandler : IRequestHandler<UpdateUserRequest, Result>
        {
            private readonly IUnitOfWork unitOfWork;

            public UpdateRequestHandler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<Result> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
            {
                var result = new Result();
                var user = await unitOfWork.User.GetById(request.Id);
                if(user is null)
                {
                    result.WithError("User not found");
                    return result;
                }

                if(!await unitOfWork.User.UserNameIsValidForUpdate(request.Id , request.UserName))
                {
                    result.WithError("userName is not valid for Update");
                    return result;
                }

                user.UserName = request.UserName;
                user.FName = request.FName;
                user.LName = request.LName;

                if(!await unitOfWork.User.Update(user))
                {
                    result.WithError("oops we have a problem here");
                    return result;
                }

                result.WithSuccess("user information updated.");
                return result;
            }
        }
    }
}
