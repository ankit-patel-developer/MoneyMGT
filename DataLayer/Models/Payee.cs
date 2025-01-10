using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class Payee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PayeeId { get; set; }
        [Required(ErrorMessage = "Payee Name is required")]
        public string PayeeName { get; set; }
        [Required(ErrorMessage = "Payee Desc is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Payee AC Number is required")]
        public string PayeeACNumber { get; set; }
        [Required(ErrorMessage = "Payee Type is required")]
        public PayeeType PayeeType { get; set; }


        // only for CreditCard type of PayeeType
        public decimal Balance { get; set; }

        public ICollection<CreditCardTransaction> CreditCardTransactions { get; set; }

    }
}
