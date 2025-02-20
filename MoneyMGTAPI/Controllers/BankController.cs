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

        // ok
        [HttpPost]
        [Route("addBank")]
        public IActionResult AddBank(Bank bank)
        {
            _response = new APIResponse();
            try
            {
                // check for null
                // bank = null;
                if (bank == null)
                {
                    return BadRequest();
                }

                // check for exception
                // throw new Exception();

                // check for ModelState
                // ModelState.AddModelError("error", "ModelState Check!");
                // ModelState.AddModelError("error", "Another ModelState Check!");
                if (ModelState.IsValid)
                {
                    _bankRepo.AddBank(bank);
                    _response.ResponseCode = 0;
                    _response.ResponseMessage = "Bank Added Successfully !";
                    _response.ResponseError = null;
                    return Ok(_response);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _response.ResponseCode = -1;
                _response.ResponseMessage = "Server Error !";
                _response.ResponseError = ex.Message.ToString();
                return Ok(_response);
            }
        }

    }
}
