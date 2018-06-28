using DaGetCore.Constants;
using DaGetCore.Service;
using DaGetCore.Service.Dto;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize(Policy = "CreateOperation")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Post([FromBody] OperationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_operationService.Create(User.Identity.GetUserId(), model));
        }

        [HttpPut]
        [Authorize(Policy = "UpdateOperation")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Put([FromBody] OperationDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_operationService.Update(User.Identity.GetUserId(), model));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteOperation")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Delete(int id)
        {
            _operationService.Delete(User.Identity.GetUserId(), id);
            return Ok();
        }
    }
}