using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;
using System.Linq;
using Services.Utility;
using Services.CustomException;

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
        // Deposit Transaction to Bank
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

        // - bank
        // + cc
        // TransactionType.Out
        // Withdraw Transaction from Bank
        public BankTransaction WithdrawToPayee(BankTransaction bankTransaction)
        {
            // 1)
            var account = appDbContext.Accounts.Where(x => x.BankId == bankTransaction.BankId && x.AccountId == bankTransaction.AccountId).FirstOrDefault();
            var currentBalance = account.Balance;
            account.Balance -= bankTransaction.TransactionAmount;
            if (account.Balance < 0)
            {
                // throw new Exception();
                throw new MinusBankBalance("Transaction Fails ! You can pay maximum of " + currentBalance);
            }

            // 2)
            var result = appDbContext.BankTransactions.Add(new BankTransaction()
            {
                PayeeId = bankTransaction.PayeeId,
                TransactionAmount = bankTransaction.TransactionAmount,
                TransactionDate = bankTransaction.TransactionDate,
                TransactionStatus = TransactionStatus.Success,
                BankId = bankTransaction.BankId,
                AccountId = bankTransaction.AccountId,
                LastBalance = currentBalance,
                CurrentBalance = account.Balance,

                RefCode = RefCodeGenerator.RandomString(6),
                TransactionType = TransactionType.Out,

                // Out transaction for bank
                // - bank
                SourceId = 0
            });

            // 3
            // check for cc
            // if cc is the payee type, then add amount @ cc Balance of Payee
            var payee = appDbContext.Payees
                            .Where(c => c.PayeeId == bankTransaction.PayeeId).FirstOrDefault();
            if (payee.PayeeType == PayeeType.CreditCard)
            {
                var ccCurrentBalance = payee.Balance;
                payee.Balance += bankTransaction.TransactionAmount;

                // 4
                // check for cc
                // if cc is the payee type, then add transaction @ CreditCardTransactions db table
                // with same RefCode generated @ BankTransactions db table
                // with transaction type In
                appDbContext.CreditCardTransactions.Add(new CreditCardTransaction()
                {
                    CreditCardId = payee.PayeeId,
                    TransactionAmount = bankTransaction.TransactionAmount,
                    TransactionDate = bankTransaction.TransactionDate,
                    TransactionStatus = TransactionStatus.Success,
                    PayeeId = payee.PayeeId,
                    LastBalance = ccCurrentBalance,
                    CurrentBalance = payee.Balance,
                    RefCode = result.Entity.RefCode,
                    TransactionType = TransactionType.In
                });
            }

            // check last moment exception
            // throw new Exception();

            appDbContext.SaveChanges();
            return result.Entity;
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
