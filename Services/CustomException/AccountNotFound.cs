using System;
using System.Collections.Generic;
using System.Text;

namespace Services.CustomException
{
    public class AccountNotFound : Exception
    {
        public AccountNotFound(string message) : base(message)
        {

        }
    }
}
