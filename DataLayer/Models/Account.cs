using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Account
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public int AccountNumber { get; set; }

        [Required(ErrorMessage = "Account Type is required")]
        public AccountType AccountType { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [ForeignKey(nameof(Bank))]
        public int BankId { get; set; }

        [JsonIgnore]
        public Bank Bank { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }
        public ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
