using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.Interfaces;

namespace Services.Repositories
{
    public class PayeeRepository : IPayeeRepository
    {
        public Payee AddPayee(Payee payee)
        {
            throw new NotImplementedException();
        }

        public Payee EditPayee(Payee payee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payee> GetAllPayees()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payee> GetAllPayeesCC()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllPayeeTypes()
        {
            throw new NotImplementedException();
        }

        public Payee GetPayee(int payeeId)
        {
            throw new NotImplementedException();
        }
    }
}
