using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        public BankTransaction AddBankTransaction(BankTransaction bankTransaction)
        {
            throw new NotImplementedException();
        }

        public AccountStatement GetAccountStatementAll(AccountVM account)
        {
            throw new NotImplementedException();
        }

        public BankStatement GetBankStatement(Bank bank)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTransactionStatusTypes()
        {
            throw new NotImplementedException();
        }
    }
}
