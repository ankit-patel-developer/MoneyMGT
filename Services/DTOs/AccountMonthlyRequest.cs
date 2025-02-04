using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Services.DTOs
{
    public class AccountMonthlyRequest
    {
        [Required]
        public int BankId { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}
