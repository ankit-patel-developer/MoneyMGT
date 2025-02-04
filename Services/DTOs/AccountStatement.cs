using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.DTOs
{
    public class AccountStatement
    {
        // Account
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal LastBalance { get; set; }

        // Bank
        public int BankId { get; set; }
        public string BankName { get; set; }

        // Transactions
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        public int BankTransactionId { get; set; }
        public int PayeeId { get; set; }
        public string PayeeName { get; set; }
        public PayeeType PayeeType { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal LastBalance { get; set; }

        // decides either In or Out
        public TransactionType TransactionType { get; set; }
        public string RefCode { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; }
    }
}
