using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account AddAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account EditAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountListVM> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllAccountTypes()
        {
            throw new NotImplementedException();
        }

        public BankAccountVM GetBankAccounts(int bankId)
        {
            throw new NotImplementedException();
        }
    }
}
