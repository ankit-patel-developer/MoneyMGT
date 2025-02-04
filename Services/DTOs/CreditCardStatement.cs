using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.DTOs
{
    public class CreditCardStatement
    {
        // CreditCard
        public int CreditCardId { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardName { get; set; }
        public PayeeType PayeeType { get; set; }
        public decimal Balance { get; set; }

        // Transactions
        public List<Transaction> Transactions { get; set; }

        public class Transaction
        {
            // Output
            // cc output transactions
            // from cc transactions
            // cc is paying off $ to payee
            public int CreditCardTransactionId { get; set; }
            public int PayeeId { get; set; }
            public string PayeeName { get; set; }
            public PayeeType PayeeType { get; set; }
            public decimal AmountPaid { get; set; }
            public DateTime TransactionDate { get; set; }
            public TransactionStatus TransactionStatus { get; set; }
            public decimal CurrentBalance { get; set; }
            public decimal LastBalance { get; set; }

            // Input
            // cc input transactions
            // from bank transactions
            // cc is getting $ from bank
            public int BankId { get; set; }
            public string BankName { get; set; }
            public int AccountId { get; set; }
            public int AccountNumber { get; set; }

            // decides either In or Out
            public TransactionType TransactionType { get; set; }

            public string RefCode { get; set; }

        }
    }
}
