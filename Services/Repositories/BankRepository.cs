using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.Interfaces;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return appDbContext.Banks;
        }

        public Bank GetBank(int bankId)
        {
            throw new NotImplementedException();
        }
    }
}
