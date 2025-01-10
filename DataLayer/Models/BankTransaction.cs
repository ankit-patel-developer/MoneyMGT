using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class BankTransaction
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankTransactionId { get; set; }

        [Required]
        public int PayeeId { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }

        // 1) insert into BankTransactions all values except TransactionStatus
        // 2) edit Balance of Accounts 
        // 3) edit TransactionStatus of BankTransactions
        public TransactionStatus TransactionStatus { get; set; }


        [JsonIgnore]
        public Bank Bank { get; set; }

        //PM> edit bank-transaction entity
        public int BankId { get; set; }

        [Required]
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

        public decimal LastBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        public string RefCode { get; set; }
        public TransactionType TransactionType { get; set; }
        public int SourceId { get; set; }

    }
}
