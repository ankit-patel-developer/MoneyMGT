using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;


namespace Services.DTOs
{
    public class AccountListVM
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
