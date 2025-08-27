using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Base
{
    //[Authorize]
    public partial class HandlerBaseController<ENT, DTO>
    {
        private readonly IConfiguration _configuration;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (!isAnonymous)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (token is not null)
                {

                }
                else
                {
                    throw new UnauthorizedAccessException("Este usuario no tiene acceso a esa acción");
                }
            }

            base.OnActionExecuting(context);
        }

        protected string GetRol()
        {
            var claims = HttpContext.User.Claims;
            var rol = claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);
            if (rol is null)
                throw new UnauthorizedAccessException("No tiene un rol asignado");
            return rol.Value;
        }
    }
}
