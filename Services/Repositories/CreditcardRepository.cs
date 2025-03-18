using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Models;
using Services.DTOs;
using Services.Interfaces;
using System.Linq;
using Services.CustomException;
using Services.Utility;

namespace Services.Repositories
{
    public class CreditcardRepository : ICreditcardRepository
    {     

        private readonly MoneyMGTContext appDbContext;

        public CreditcardRepository(MoneyMGTContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<CreditCard> GetAllCCs()
        {
            List<CreditCard> ccs = new List<CreditCard>();
            var payeesDb = appDbContext.Payees
                           .Where(x => x.PayeeType == PayeeType.CreditCard);
            if (payeesDb != null)
            {
                foreach (var payee in payeesDb)
                {
                    ccs.Add(new CreditCard()
                    {
                        Balance = payee.Balance,
                        CreditCardId = payee.PayeeId,
                        CreditCardName = payee.PayeeName,
                        CreditCardNumber = payee.PayeeACNumber,
                        PayeeType = payee.PayeeType
                    });
                }
            }
            return ccs;
        }

        // - cc
        // TransactionType.Out
        public CreditCardTransaction PayByCreditCard(CreditCardTransaction ccTransaction)
        {
            // 0)
            // check for Payee and CreditCard Exists or not
            var _payee = appDbContext.Payees.Where(x => x.PayeeId == ccTransaction.PayeeId).FirstOrDefault();
            var _cc = appDbContext.Payees.Where(x => x.PayeeId == ccTransaction.CreditCardId && x.PayeeType == PayeeType.CreditCard).FirstOrDefault();
            if (_payee == null || _cc == null)
                throw new CreditCardNotFound("Unknown Payee Or CreditCard !");

            // 1)
            var cc = appDbContext.Payees.Where(x => x.PayeeId == ccTransaction.CreditCardId).FirstOrDefault();
            var currentBalance = cc.Balance;
            cc.Balance -= ccTransaction.TransactionAmount;
            if (cc.Balance < 0)
            {
                // throw new Exception();
                throw new MinusCCBalance("Transaction Fails ! You can pay maximum of " + currentBalance);
            }

            // 2)
            var result = appDbContext.CreditCardTransactions.Add(new CreditCardTransaction()
            {
                PayeeId = ccTransaction.PayeeId,
                TransactionAmount = ccTransaction.TransactionAmount,
                TransactionDate = ccTransaction.TransactionDate,
                TransactionStatus = TransactionStatus.Success,
                CreditCardId = ccTransaction.CreditCardId,
                LastBalance = currentBalance,
                CurrentBalance = cc.Balance,

                RefCode = RefCodeGenerator.RandomString(6),
                TransactionType = TransactionType.Out
            });

            // check last moment exception
            // throw new Exception();

            appDbContext.SaveChanges();
            return result.Entity;
        }

        public CreditCardStatement GetCreditCardStatementAll(CreditCard cc)
        {
            throw new NotImplementedException();
        }
    }
}
