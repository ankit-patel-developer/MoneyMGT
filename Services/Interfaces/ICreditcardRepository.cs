using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface ICreditcardRepository
    {
        IEnumerable<CreditCard> GetAllCCs();
        CreditCardStatement GetCreditCardStatementAll(CreditCard cc);
        CreditCardTransaction AddCCTransaction(CreditCardTransaction ccTransaction);
    }
}
