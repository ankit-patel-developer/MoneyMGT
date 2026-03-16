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
    public class VirtualTransactionController : ControllerBase
    {
        private readonly IVirtualTransactionsRepository _vtRepo;


        public VirtualTransactionController(IVirtualTransactionsRepository vtRepo)
        {
            _vtRepo = vtRepo;
        }

        // wip
        [HttpPost]
        [Route("depositVT")]
        public IActionResult DepositVT(VTObject vtObject)        
        {
            
            try
            {
                // check for null
                // vtObject = null;
                if (vtObject == null)
                {
                    return BadRequest();
                }

                // check for exception
                // throw new Exception();             

                return Ok(_vtRepo.DepositVTAsync(vtObject));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }    

    }
}
