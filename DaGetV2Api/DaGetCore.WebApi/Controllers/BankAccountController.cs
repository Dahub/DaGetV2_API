using DaGetCore.Service;
using DaGetCore.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DaGetCore.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/BankAccount")]
    public class BankAccountController : Controller
    {
        private IBankAccountService _bankAccountService;

        public BankAccountController([FromServices] IBankAccountService ba)
        {
            _bankAccountService = ba;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadBankAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(Policy = "CreateBankAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Post([FromBody] BankAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_bankAccountService.Create(User.Identity.GetUserId(), User.Identity.Name, model));
        }
    }
}