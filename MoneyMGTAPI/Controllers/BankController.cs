using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMGTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepo;
        private APIResponse _response;

        public BankController(IBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }

        // ok
        [HttpGet]
        [Route("allBanks")]
        public IActionResult GetAllBanks()
        {
            var allBanks = _bankRepo.GetAllBanks();
            return Ok(allBanks);
        }
    }
}
