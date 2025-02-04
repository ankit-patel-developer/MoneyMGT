using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.DTOs
{
    public class BankStatement
    {
        // Bank
        public int BankId { get; set; }
        public string BankName { get; set; }

        // BankAccounts
        public List<BankAccount> BankAccounts { get; set; }
    }
    public class BankAccount
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal LastBalance { get; set; }
        // Transactions
        public List<Transaction> Transactions { get; set; }
    }
}
