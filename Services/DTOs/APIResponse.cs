using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class APIResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseError { get; set; }
    }
}
