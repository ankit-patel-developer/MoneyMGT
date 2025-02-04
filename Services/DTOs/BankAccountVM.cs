using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class BankAccountVM
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public List<AccountVM> Accounts { get; set; }
    }
}
