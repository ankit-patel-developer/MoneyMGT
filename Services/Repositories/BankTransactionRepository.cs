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
