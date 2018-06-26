using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaGetCore.Constants;
using DaGetCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaGetCore.WebApi.Controllers
{
    [Produces("application/json")]
    [Route(Routes.bankAccount + "/{bankAccountId}/" + Routes.operation)] 
    public class OperationController : Controller
    {
        private IOperationService _operationService;

        public OperationController([FromServices] IOperationService os)
        {
            _operationService = os;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOperation")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get(int bankAccountId, int id)
        {
            return Ok(_operationService.GetById(User.Identity.GetUserId(), id));
        }
    }
}