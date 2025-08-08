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
using Microsoft.EntityFrameworkCore;

namespace Services.Repositories
{
    public class VirtualTransactionsRepository : IVirtualTransactionsRepository
    {
        private readonly MoneyMGTContext appDbContext;

        public VirtualTransactionsRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        // + bank from source
        // TransactionType.In
        // Deposit Transaction to Bank
        public bool DepositFromSource()
        {
            /*
            
Alter PROCEDURE VT_DEPOSIT 
      @BankId int,  
	  @AccountNumber int
AS 
BEGIN
 
    SET NOCOUNT ON; 
	
	declare @currentBalance decimal(18,2);
	-- find current account balance
	select @currentBalance = Balance
	from Accounts
	where BankId=@BankId and AccountNumber=@AccountNumber;
	print 'current balance for a/c no = ' + cast(@AccountNumber as varchar(20)) + ' = $' + cast(@currentBalance as varchar(20));

    -- OUTPUT> current balance for a/c no = 11223344 = $2549.50

END
GO   
            */

            return true;
        }

        // - bank
        // + cc
        // TransactionType.Out
        // Withdraw Transaction from Bank
        public bool WithdrawToPayee()
        {
        
            return true;
        }

        public AccountStatement GetAccountStatement(AccountVM account)
        {
            AccountStatement acStatement = new AccountStatement();    
            acStatement.Transactions = new List<Transaction>();

            var acTransactions = appDbContext.BankTransactions.Where(x => x.AccountId == account.AccountId);
            if (acTransactions != null && acTransactions.Count() >= 1)
            {
                // -/+ bank
                // Transactions
                // last 50 transactions
                foreach (var transaction in acTransactions.OrderByDescending(x => x.TransactionDate).Take(50))
                {
                    // - bank
                    // payee
                    if (transaction.SourceId == 0)
                    {
                        // Payee
                        var payee = appDbContext.Payees
                                        .Where(b => b.PayeeId == transaction.PayeeId).FirstOrDefault();

                        acStatement.Transactions.Add(new Transaction()
                        {
                            BankTransactionId = transaction.BankTransactionId,
                            PayeeId = transaction.PayeeId,
                            PayeeName = payee.PayeeName,
                            PayeeType = payee.PayeeType,
                            AmountPaid = transaction.TransactionAmount,
                            TransactionDate = transaction.TransactionDate,
                            TransactionStatus = transaction.TransactionStatus,
                            CurrentBalance = transaction.CurrentBalance,
                            LastBalance = transaction.LastBalance,
                            RefCode = transaction.RefCode,
                            TransactionType = transaction.TransactionType,
                            SourceId = transaction.SourceId,
                            SourceName = "N/A"
                        });
                    }
                    // + bank
                    // source
                    else
                    {
                        // Source
                        var source = appDbContext.Sources
                                        .Where(b => b.SourceId == transaction.SourceId).FirstOrDefault();

                        acStatement.Transactions.Add(new Transaction()
                        {
                            BankTransactionId = transaction.BankTransactionId,
                            PayeeId = 0,
                            PayeeName = "N/A",
                            PayeeType = PayeeType.Others,
                            AmountPaid = transaction.TransactionAmount,
                            TransactionDate = transaction.TransactionDate,
                            TransactionStatus = transaction.TransactionStatus,
                            CurrentBalance = transaction.CurrentBalance,
                            LastBalance = transaction.LastBalance,
                            RefCode = transaction.RefCode,
                            TransactionType = transaction.TransactionType,
                            SourceId = transaction.SourceId,
                            SourceName = source.SourceName
                        });
                    }
                }

            }
            return acStatement;
        }

        // returns for all accounts of selected bank
        // - bank
        // TransactionType.Out
        // + bank from source
        // TransactionType.In
        public BankStatement GetBankStatement(Bank bank)
        {
            BankStatement bankStatement = new BankStatement();
            List<BankAccount> bankAccounts = new List<BankAccount>();
            bankStatement.BankAccounts = bankAccounts;
            List<Transaction> transactions = new List<Transaction>();

            bankStatement.BankId = bank.BankId;
            bankStatement.BankName = bank.BankName;

            // finding accounts of bank
            var accounts = appDbContext.Accounts
                                .Include(y => y.BankTransactions)
                                .Where(x => x.BankId == bank.BankId);

            if (accounts != null && accounts.Count()>=1)
            {
                foreach (var account in accounts)
                {
                    // adding accounts to bank
                    BankAccount bankAccount = new BankAccount();
                    bankAccount.AccountId = account.AccountId;
                    bankAccount.AccountNumber = account.AccountNumber;
                    bankAccount.AccountType = account.AccountType;
                    bankAccount.LastBalance = account.Balance;
                    bankAccount.Transactions = new List<Transaction>();

                    // finding transactions of bank-account
                    if (account.BankTransactions != null && account.BankTransactions.Count() >= 1)
                    {
                        // -/+ bank
                        // Transactions
                        // last 10 transactions
                        foreach (var transaction in account.BankTransactions.OrderByDescending(x=>x.TransactionDate).Take(10))
                        {
                            // - bank
                            // payee
                            if (transaction.SourceId == 0)
                            {
                                // Payee
                                var payee = appDbContext.Payees
                                                .Where(b => b.PayeeId == transaction.PayeeId).FirstOrDefault();

                                bankAccount.Transactions.Add(new Transaction()
                                {
                                    BankTransactionId = transaction.BankTransactionId,
                                    PayeeId = transaction.PayeeId,
                                    PayeeName = payee.PayeeName,
                                    PayeeType = payee.PayeeType,
                                    AmountPaid = transaction.TransactionAmount,
                                    TransactionDate = transaction.TransactionDate,
                                    TransactionStatus = transaction.TransactionStatus,
                                    CurrentBalance = transaction.CurrentBalance,
                                    LastBalance = transaction.LastBalance,
                                    RefCode = transaction.RefCode,
                                    TransactionType = transaction.TransactionType,
                                    SourceId = transaction.SourceId,
                                    SourceName = "N/A"
                                });
                            }
                            // + bank
                            // source
                            else
                            {
                                // Source
                                var source = appDbContext.Sources
                                                .Where(b => b.SourceId == transaction.SourceId).FirstOrDefault();

                                bankAccount.Transactions.Add(new Transaction()
                                {
                                    BankTransactionId = transaction.BankTransactionId,
                                    PayeeId = 0,
                                    PayeeName = "N/A",
                                    PayeeType = PayeeType.Others,
                                    AmountPaid = transaction.TransactionAmount,
                                    TransactionDate = transaction.TransactionDate,
                                    TransactionStatus = transaction.TransactionStatus,
                                    CurrentBalance = transaction.CurrentBalance,
                                    LastBalance = transaction.LastBalance,
                                    RefCode = transaction.RefCode,
                                    TransactionType = transaction.TransactionType,
                                    SourceId = transaction.SourceId,
                                    SourceName = source.SourceName
                                });
                            }
                        }
                    }
                    bankAccounts.Add(bankAccount);
                }
            }
            else
            {
                throw new AccountNotFound("Bank has No Account Yet !");
            }
            return bankStatement;
        }

        public List<string> GetTransactionStatusTypes()
        {
            throw new NotImplementedException();
        }
    }
}
