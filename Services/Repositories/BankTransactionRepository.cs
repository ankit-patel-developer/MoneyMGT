using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;
using System.Linq;
using Services.Utility;

namespace Services.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public BankTransactionRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // + bank from source
        // TransactionType.In
        // Deposit Transaction
        public BankTransaction DepositFromSource(BankTransaction bankTransaction)
        {
            // 1)
            var account = appDbContext.Accounts.Where(x => x.BankId == bankTransaction.BankId && x.AccountId == bankTransaction.AccountId).FirstOrDefault();
            var currentBalance = account.Balance;
            account.Balance += bankTransaction.TransactionAmount;

            // 2)
            var result = appDbContext.BankTransactions.Add(new BankTransaction()
            {
                // + bank
                // so payeeId = 0
                PayeeId = 0,

                TransactionAmount = bankTransaction.TransactionAmount,
                TransactionDate = bankTransaction.TransactionDate,
                TransactionStatus = TransactionStatus.Success,
                BankId = bankTransaction.BankId,
                AccountId = bankTransaction.AccountId,
                LastBalance = currentBalance,
                CurrentBalance = account.Balance,

                RefCode = RefCodeGenerator.RandomString(6),
                TransactionType = TransactionType.In,

                // In transaction for bank
                // + bank
                SourceId = bankTransaction.SourceId
            });

            // check last moment exception
            // throw new Exception();

            appDbContext.SaveChanges();
            return result.Entity;
        }
        public BankTransaction AddBankTransaction(BankTransaction bankTransaction)
        {
            throw new NotImplementedException();
        }

        public AccountStatement GetAccountStatementAll(AccountVM account)
        {
            throw new NotImplementedException();
        }

        public BankStatement GetBankStatement(Bank bank)
        {
            throw new NotImplementedException();
        }

        public List<string> GetTransactionStatusTypes()
        {
            throw new NotImplementedException();
        }
    }
}
