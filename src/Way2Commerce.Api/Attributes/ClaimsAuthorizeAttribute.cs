using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Way2Commerce.Api.Attributes
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter)) =>
            Arguments = new object[] { new Claim(claimType, claimValue) };
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim) =>
            _claim = claim;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User as ClaimsPrincipal;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!user.HasClaim(_claim.Type, _claim.Value))
                context.Result = new ForbidResult();
        }
    }
}