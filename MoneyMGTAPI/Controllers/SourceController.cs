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
    public class SourceController : ControllerBase
    {
        private APIResponse _response;
        private readonly ISourceRepository _sourceRepo;

        public SourceController(ISourceRepository sourceRepo)
        {
            _sourceRepo = sourceRepo;
        }

        // ok
        [HttpGet]
        [Route("allSources")]
        public IActionResult GetAllSources()
        {
            var allSources = _sourceRepo.GetAllSources();
            return Ok(allSources);
        }
    }
}
