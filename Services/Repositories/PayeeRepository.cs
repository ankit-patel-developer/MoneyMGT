using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.Interfaces;
using System.Linq;

namespace Services.Repositories
{
    public class PayeeRepository : IPayeeRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public PayeeRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public Payee AddPayee(Payee payee)
        {
            var result = appDbContext.Payees.Add(payee);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public Payee EditPayee(Payee payee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payee> GetAllPayees()
        {
            List<Payee> payees = new List<Payee>();
            var payeesDb = appDbContext.Payees;
            if (payeesDb != null)
            {
                payees = payeesDb.ToList();
            }
            return payees;
        }

        public IEnumerable<Payee> GetAllPayeesCC()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllPayeeTypes()
        {
            List<string> payeeTypes = new List<string>();
            foreach (string payeeType in Enum.GetNames(typeof(PayeeType)))
            {
                payeeTypes.Add(payeeType);
            }
            return payeeTypes;
        }

        public Payee GetPayee(int payeeId)
        {
            throw new NotImplementedException();
        }
    }
}
