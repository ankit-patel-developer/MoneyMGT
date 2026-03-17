using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class VTObject
    {
        public int BankId { get; set; }
        public int AccountNumber { get; set; }
        public int NumberOfTransactions { get; set; }
        public bool TransactionResponse { get; set; }

    }
}
