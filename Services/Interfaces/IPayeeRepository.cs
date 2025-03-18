using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.Interfaces
{
    public interface IPayeeRepository
    {
        IEnumerable<Payee> GetAllPayees();
        Payee AddPayee(Payee payee);
        List<string> GetAllPayeeTypes();
        IEnumerable<Payee> GetAllPayeesForCC();
        Payee GetPayee(int payeeId);
        Payee EditPayee(Payee payee);
    }
}
