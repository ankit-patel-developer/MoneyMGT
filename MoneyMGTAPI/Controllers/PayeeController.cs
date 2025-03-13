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
    public class PayeeController : ControllerBase
    {
        private APIResponse _response;
        private readonly IPayeeRepository _payeeRepo;

        public PayeeController(IPayeeRepository payeeRepo)
        {
            _payeeRepo = payeeRepo;
        }

        // ok
        [HttpGet]
        [Route("allPayees")]
        public IActionResult GetAllPayees()
        {
            var allPayees = _payeeRepo.GetAllPayees();
            return Ok(allPayees);
        }
    }
}
