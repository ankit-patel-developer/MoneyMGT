using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.Interfaces;
using System.Linq;

namespace Services.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public BankRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Bank AddBank(Bank bank)
        {
            var result = appDbContext.Banks.Add(bank);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public Bank EditBank(Bank bank)
        {
            var result = appDbContext.Banks.Where(x => x.BankId == bank.BankId).FirstOrDefault();
            if (result != null)
            {
                result.BankName = bank.BankName;

                appDbContext.SaveChanges();
                return bank;

                // check for null
                // return null;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return appDbContext.Banks;
        }

        public Bank GetBank(int bankId)
        {
            return appDbContext.Banks.Where(x => x.BankId == bankId).FirstOrDefault();
        }
    }
}
