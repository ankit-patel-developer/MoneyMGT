using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.Interfaces
{
    public interface IBankRepository
    {
        IEnumerable<Bank> GetAllBanks();
        Bank AddBank(Bank bank);
        Bank GetBank(int bankId);
        Bank EditBank(Bank bank);
    }
}
