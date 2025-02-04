using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.DTOs
{
    public class AccountMonthly
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string Period { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public TransactionType TranType { get; set; }
        public decimal Total { get; set; }
    }
}
