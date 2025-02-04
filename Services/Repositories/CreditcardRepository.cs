using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Repositories
{
    public class CreditcardRepository : ICreditcardRepository
    {
        public CreditCardTransaction AddCCTransaction(CreditCardTransaction ccTransaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditCard> GetAllCCs()
        {
            throw new NotImplementedException();
        }

        public CreditCardStatement GetCreditCardStatementAll(CreditCard cc)
        {
            throw new NotImplementedException();
        }
    }
}
