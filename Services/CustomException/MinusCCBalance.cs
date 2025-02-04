using System;
using System.Collections.Generic;
using System.Text;

namespace Services.CustomException
{
    public class MinusCCBalance : Exception
    {
        public MinusCCBalance(string message) : base(message)
        {

        }
    }
}
