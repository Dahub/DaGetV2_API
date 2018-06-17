using DaGetCore.Service.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DaGetCore.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Error")]
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult Get()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                Exception ex = exceptionFeature.Error;

                if (ex.GetType() == typeof(DaGetServiceException))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        String.Format("Une erreur non prise en charge s'est produite : {0}", ex.Message));
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}