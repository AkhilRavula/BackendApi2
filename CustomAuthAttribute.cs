using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackendApi2;
public class CustomAuthorization :  AuthorizeAttribute, IAuthorizationFilter
{
   public void OnAuthorization(AuthorizationFilterContext context)
   {
        var user = context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(
                new { message = "Unauthorized=>Only Admin can access" }) 
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
   }
}
