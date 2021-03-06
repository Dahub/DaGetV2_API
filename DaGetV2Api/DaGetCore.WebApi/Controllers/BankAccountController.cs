﻿using DaGetCore.Constants;
using DaGetCore.Service;
using DaGetCore.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DaGetCore.WebApi.Controllers
{
    [Produces("application/json")]
    [Route(Routes.bankAccount)]
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
            return Ok(_bankAccountService.GetById(User.Identity.GetUserId(), id));
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
        
        [HttpPut]
        [Authorize(Policy = "UpdateBankAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Put([FromBody] BankAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_bankAccountService.Update(User.Identity.GetUserId(), model));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteBankAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Delete(int id)
        {
            _bankAccountService.Delete(User.Identity.GetUserId(), id);
            return Ok();
        }

        [HttpGet("{id}/" + Routes.bankAccountType)]
        [Authorize(Policy = "ReadBankAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult BankAccountType(int id)
        {
            return Ok(_bankAccountService.GetBankAccountType(User.Identity.GetUserId(), id));
        }
    }
}