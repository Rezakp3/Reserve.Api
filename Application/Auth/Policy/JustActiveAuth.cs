using Domain;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authentication.Policy
{
    public record IsActiveRequirement : IAuthorizationRequirement;
    public class JustActiveAuth 
        : AuthorizationHandler<IsActiveRequirement>
    {
        private readonly IUnitOfWork unitOfWork;

        public JustActiveAuth(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, IsActiveRequirement requirement)
        {
            //if (unitOfWork.User.GetUserActive(Guid.Parse(context.User.Identity.Name)))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
