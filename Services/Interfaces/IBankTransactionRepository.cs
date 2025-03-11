using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IBankTransactionRepository
    {
        BankTransaction DepositFromSource(BankTransaction bankTransaction);
        BankTransaction AddBankTransaction(BankTransaction bankTransaction);
        List<string> GetTransactionStatusTypes();
        AccountStatement GetAccountStatementAll(AccountVM account);
        BankStatement GetBankStatement(Bank bank);
    }
}
