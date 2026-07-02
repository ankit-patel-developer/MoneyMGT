using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IVirtualTransactionsRepository
    {
        VTObject DepositVT(VTObject vtObject);

        // Payee includes CC and other types of Payees
        VTObject WithdrawVT(VTObject vtObject);
    }
}
