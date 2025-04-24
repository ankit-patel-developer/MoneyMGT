using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CustomException;
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
    public class BankTransactionController : ControllerBase
    {
        private APIResponse _response;
        private readonly IBankTransactionRepository _transactionRepo;


        public BankTransactionController(IBankTransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        // ok
        [HttpPost]
        [Route("depositFromSource")]
        public IActionResult DepositFromSource(BankTransaction bankTransaction)
        {
            _response = new APIResponse();
            try
            {
                // check for null
                // bankTransaction = null;
                if (bankTransaction == null)
                {
                    return BadRequest();
                }

                // check for exception
                // throw new Exception();             

                if (ModelState.IsValid)
                {
                    _transactionRepo.DepositFromSource(bankTransaction);
                    _response.ResponseCode = 0;
                    _response.ResponseMessage = "Deposit made Successfully !";
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
        [HttpPost]
        [Route("withdrawToPayee")]
        public IActionResult WithdrawToPayee(BankTransaction bankTransaction)
        {
            _response = new APIResponse();
            try
            {
                // check for null
                // bankTransaction = null;
                if (bankTransaction == null)
                {
                    return BadRequest();
                }

                // check for exception
                // throw new Exception();

                // check for ModelState
                // ModelState.AddModelError("error", "ModelState Check!");
                // ModelState.AddModelError("error", "Another ModelState Check!");
                // ModelState.AddModelError("error", "One More Another ModelState Check!");

                if (ModelState.IsValid)
                {
                    _transactionRepo.WithdrawToPayee(bankTransaction);
                    _response.ResponseCode = 0;
                    _response.ResponseMessage = "Withdraw made Successfully !";
                    _response.ResponseError = null;
                    return Ok(_response);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (MinusBankBalance mbb)
            {
                _response.ResponseCode = -1;
                _response.ResponseMessage = mbb.Message.ToString();
                _response.ResponseError = mbb.Message.ToString();
                return Ok(_response);
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
        [HttpPost]
        [Route("getBankStatement")]
        public IActionResult GetBankStatement(Bank bank)
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

                return Ok(_transactionRepo.GetBankStatement(bank));
            }
            catch (AccountNotFound noAcc)
            {
                _response.ResponseCode = -1;
                _response.ResponseMessage = noAcc.Message.ToString();
                _response.ResponseError = noAcc.Message.ToString();
                return Ok(_response);
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
