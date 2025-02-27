using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.Interfaces;
using System.Linq;
using Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public AccountRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<AccountListVM> GetAllAccounts()
        {
            List<AccountListVM> accounts = new List<AccountListVM>();
            var accountsDb = appDbContext.Accounts.Include(x => x.Bank);
            if (accountsDb != null)
            {
                foreach (var ac in accountsDb)
                {
                    accounts.Add(new AccountListVM()
                    {
                        AccountId = ac.AccountId,
                        AccountType = ac.AccountType,
                        AccountNumber = ac.AccountNumber,
                        Balance = ac.Balance,
                        BankId = ac.BankId,
                        BankName = ac.Bank.BankName
                    });
                }
            }
            return accounts;
        }

        public Account AddAccount(Account account)
        {
            var result = appDbContext.Accounts.Add(account);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public Account EditAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }    

        public List<string> GetAllAccountTypes()
        {
            throw new NotImplementedException();
        }

        IEnumerable<AccountListVM> IAccountRepository.GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public BankAccountVM GetBankAccounts(int bankId)
        {
            BankAccountVM bankAccountVM = new BankAccountVM();
            List<AccountVM> accounts = new List<AccountVM>();
            bankAccountVM.Accounts = accounts;
            bankAccountVM.BankId = bankId;
            var bankDb = appDbContext.Banks.Where(x => x.BankId == bankId).FirstOrDefault();

            if (bankDb != null)
            {
                bankAccountVM.BankName = bankDb.BankName;

                var accountsDb = appDbContext.Accounts.Where(x => x.BankId == bankId).Include(y => y.Bank);
                if (accountsDb != null)
                {
                    foreach (var ac in accountsDb)
                    {
                        accounts.Add(new AccountVM()
                        {
                            AccountId = ac.AccountId,
                            AccountType = ac.AccountType,
                            AccountNumber = ac.AccountNumber,
                            Balance = ac.Balance
                        });
                    }
                }
                return bankAccountVM;
            }
            else
            {
                return bankAccountVM;
            }
        }
    }
}
