using System;
using System.Collections.Generic;
using System.Text;

namespace Services.CustomException
{
    public class CreditCardNotFound : Exception
    {
        public CreditCardNotFound(string message) : base(message)
        {

        }
    }
}
