using DataLayer.Models;
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
    public class AccountController : ControllerBase
    {
        private APIResponse _response;
        private readonly IAccountRepository _acRepo;

        public AccountController(IAccountRepository acRepo)
        {
            _acRepo = acRepo;
        }

        // ok
        [HttpGet]
        [Route("getBankAccounts/{bankId}")]
        public IActionResult GetBankAccounts(int bankId)
        {
            var bankAccounts = _acRepo.GetBankAccounts(bankId);
            return Ok(bankAccounts);
        }
    }
}
