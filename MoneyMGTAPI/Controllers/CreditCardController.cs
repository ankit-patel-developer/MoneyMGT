using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.CustomException;


namespace MoneyMGTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private APIResponse _response;
        private readonly ICreditcardRepository _ccRepo;
        private readonly IPayeeRepository _payeeRepo;

        public CreditCardController(ICreditcardRepository ccRepo, IPayeeRepository payeeRepo)
        {
            _ccRepo = ccRepo;
            _payeeRepo = payeeRepo;
        }

        // ok
        [HttpGet]
        [Route("allCCs")]
        public IActionResult GetAllCCs()
        {
            var allCCs = _ccRepo.GetAllCCs();
            return Ok(allCCs);
        }

        // ok
        [HttpGet]
        [Route("allPayeesForCCs")]
        public IActionResult GetAllPayeesForCCs()
        {
            var allCCs = _payeeRepo.GetAllPayeesForCC();
            return Ok(allCCs);
        }

        // ok
        [HttpPost]
        [Route("payByCreditCard")]
        public IActionResult PayByCreditCard(CreditCardTransaction ccTransaction)
        {
            _response = new APIResponse();
            try
            {
                // check for null
                // ccTransaction = null;
                if (ccTransaction == null)
                {
                    return BadRequest();
                }

                // check for exception
                // throw new Exception();

                // check for ModelState
                // ModelState.AddModelError("CreditCardId", "CreditCardId is Required!");
                // ModelState.AddModelError("error", "Another ModelState Check!");
                // ModelState.AddModelError("error", "One More Another ModelState Check!");

                if (ModelState.IsValid)
                {
                    _ccRepo.PayByCreditCard(ccTransaction);
                    _response.ResponseCode = 0;
                    _response.ResponseMessage = "Pay with Credit-Card made Successfully !";
                    _response.ResponseError = null;
                    return Ok(_response);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (CreditCardNotFound ccnf)
            {
                _response.ResponseCode = -1;
                _response.ResponseMessage = "Unknown Payee Or CreditCard !";
                _response.ResponseError = ccnf.Message.ToString();
                return Ok(_response);
            }
            catch (MinusCCBalance mccb)
            {
                _response.ResponseCode = -1;
                _response.ResponseMessage = mccb.Message.ToString();
                _response.ResponseError = mccb.Message.ToString();
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
