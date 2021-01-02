using System.Collections.Generic;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Transfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITrnsferService _trnsferService;

        public TransferController(ITrnsferService trnsferService)
        {
            _trnsferService = trnsferService;
        }
        // GET
        [HttpGet("getAllAccounts")]
        public ActionResult<IEnumerable<TransferLog>>Get()
        {
            return Ok( _trnsferService.GetTrnsferLog());
        }
    }
}