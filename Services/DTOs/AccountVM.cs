using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

namespace Services.DTOs
{
    public class AccountVM
    {
        [Required(ErrorMessage = "Account is required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
