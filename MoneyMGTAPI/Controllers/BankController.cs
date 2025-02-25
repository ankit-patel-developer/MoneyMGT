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


        // ok
        [HttpGet]
        [Route("getBank/{selectedBankId}")]
        public IActionResult GetBank(int selectedBankId)
        {
            try
            {
                var bank = _bankRepo.GetBank(selectedBankId);
                // check for null
                // bank = null;

                // check for exception
                // throw new Exception();

                if (bank == null)
                {
                    return BadRequest("Bank Not Found!");
                }
                else
                {
                    return Ok(bank);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Server Error!");
            }
        }

        // ok
        [HttpPost]
        [Route("editBank")]
        public IActionResult EditBank(Bank bank)
        {
            _response = new APIResponse();
            try
            {
                // check for null
                // bank = null;
                if (bank == null)
                {
                    // return BadRequest();
                    return BadRequest("Bad Request!");
                }

                // check for exception
                // throw new Exception();

                // check for ModelState
                // ModelState.AddModelError("error", "ModelState Check!");
                // ModelState.AddModelError("error", "Another ModelState Check!");
                // ModelState.AddModelError("error", "One More Another ModelState Check!");
                if (ModelState.IsValid)
                {
                    Bank editedBank = _bankRepo.EditBank(bank);
                    // check null output from service
                    // editedBank = null;
                    if (editedBank == null)
                    {
                        // bank not found
                        // data mis-match either at client or server side
                        _response.ResponseCode = -1;
                        _response.ResponseMessage = "Bank Not Found @ Server Side!";
                        _response.ResponseError = "Bank Not Found @ Server Side!";
                        return Ok(_response);
                    }
                    else
                    {
                        // success
                        _response.ResponseCode = 0;
                        _response.ResponseMessage = "Bank Edited Successfully!";
                        _response.ResponseError = null;
                        return Ok(_response);
                    }
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
