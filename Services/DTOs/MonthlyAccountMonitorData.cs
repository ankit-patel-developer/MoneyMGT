using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

namespace Services.DTOs
{
    public class MonthlyAccountMonitorData
    {
        public string BankName { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public List<TransactionData> Transactions { get; set; }
    
    }

    public class TransactionData
    {
        public int BankTransactionId { get; set; }
        public TransactionType TranType { get; set; }
        public decimal Amount { get; set; }
    }
}
