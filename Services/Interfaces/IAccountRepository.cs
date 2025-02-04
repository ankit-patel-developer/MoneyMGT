using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using Services.DTOs;


namespace Services.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<AccountListVM> GetAllAccounts();
        Account AddAccount(Account account);
        Account GetAccount(int accountId);
        Account EditAccount(Account account);
        BankAccountVM GetBankAccounts(int bankId);
        List<string> GetAllAccountTypes();

    }
}
