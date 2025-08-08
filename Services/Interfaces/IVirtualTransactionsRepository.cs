using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IVirtualTransactionsRepository
    {
        bool DepositFromSource();

        // Payee includes CC and other types of Payees
        bool WithdrawToPayee();    
    }
}
