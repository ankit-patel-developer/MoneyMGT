using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.DTOs
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardNumber { get; set; }
        public PayeeType PayeeType { get; set; }
        public decimal Balance { get; set; }
    }
}
