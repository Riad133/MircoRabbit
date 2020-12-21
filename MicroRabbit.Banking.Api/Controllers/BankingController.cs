using System.Collections.Generic;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET
        [HttpGet("getAllAccounts")]
        public ActionResult<IEnumerable<Account>>Get()
        {
            return Ok( _accountService.GetAccounts());
        }
    }
}