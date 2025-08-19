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
            


exec VT_DEPOSIT 1, 11223344

Alter PROCEDURE VT_DEPOSIT 
      @BankId int, 
	  @AccountNumber int	   
AS 
BEGIN
 
    SET NOCOUNT ON; 
	
	declare @currentBalance_ decimal(18,2);
	declare @addBalance decimal(18,2);
	declare @updatedBalance decimal(18,2);

	declare @AccountId int;
	
	declare @PayeeId int;
	declare @TransactionAmount decimal(18,2);
	declare @TransactionDate datetime; -- current datetime
	declare @TransactionStatus int;
	declare @LastBalance decimal(18,2);
	declare @CurrentBalance decimal(18,2);
	declare @RefCode varchar(6);
	declare @TransactionType int;
	declare @SourceId int;


	-- 10 TRANSACTIONS 
	declare @cnt int = 0;
	WHILE @cnt<10
	BEGIN
		-- find current account balance
		select @currentBalance_ = Balance, @AccountId = AccountId
		from Accounts
		where BankId=@BankId and AccountNumber=@AccountNumber;
		print 'current balance for a/c no = ' + cast(@AccountNumber as varchar(20)) + ' = $' + cast(@currentBalance_ as varchar(20));

		-- random generate amount to be deposit
		-- random decimal between 0 and 1000, rounded to 2 decimal places
		select @addBalance = ROUND(RAND() * 1000, 2);
		select @updatedBalance = @currentBalance_ + @addBalance;
		print 'add balance = ' + cast(@addBalance as varchar(20))
		print 'updated balance for a/c no = ' + cast(@AccountNumber as varchar(20)) + ' = $' + cast(@updatedBalance as varchar(20));

		-- + bank
		-- PayeeId = 0
		select @PayeeId = 0;
		select @TransactionAmount = @addBalance;
		select @TransactionDate = CURRENT_TIMESTAMP;
		select @TransactionStatus = 0; -- success
		select @LastBalance = @currentBalance_;
		select @CurrentBalance = @updatedBalance;
		-- SELECT SUBSTRING(CONVERT(varchar(255), NEWID()), 0, 7);
		select @RefCode = SUBSTRING(CONVERT(varchar(255), NEWID()), 0, 7);
		select @TransactionType = 0; -- IN

		-- random generate SourceId (1-4)
		-- random integer between 1 and 4 (inclusive)
		select @SourceId = FLOOR(RAND() * 4) + 1;

		print 'payee id = ' + cast(@PayeeId as varchar(20));
		print 'transaction amount = ' + cast(@TransactionAmount as varchar(20));
		print 'transaction date = ' + cast(@TransactionDate as varchar(20));
		print 'transaction status = ' + cast(@TransactionStatus as varchar(20));
		print 'bank id = ' + cast(@BankId as varchar(20));
		print 'account id = ' + cast(@AccountId as varchar(20));
		print 'last balance = ' + cast(@LastBalance as varchar(20));
		print 'current balance = ' + cast(@CurrentBalance as varchar(20));
		print 'ref code = ' + cast(@RefCode as varchar(6));
		print 'transaction type = ' + cast(@TransactionType as varchar(20));
		print 'source id = ' + cast(@SourceId as varchar(20));


		-- sql transaction - try/catch
		BEGIN TRY
			BEGIN TRANSACTION;
			
				-- update Balance column @ Accounts table
				update Accounts
				set Balance = @CurrentBalance
				where AccountId=@AccountId and AccountNumber = @AccountNumber;

				-- insert @ BankTransactions table
				insert into BankTransactions
				(PayeeId, TransactionAmount, TransactionDate, TransactionStatus, BankId, AccountId, LastBalance, CurrentBalance, RefCode, TransactionType,SourceId )
				values
				(@PayeeId, @TransactionAmount, @TransactionDate, @TransactionStatus, @BankId, @AccountId, @LastBalance, @CurrentBalance, @RefCode, @TransactionType, @SourceId);
				
				-- check for exception
				-- (@PayeeId, 'xxx', @TransactionDate, @TransactionStatus, @BankId, @AccountId, @LastBalance, @CurrentBalance, @RefCode, @TransactionType, @SourceId);
			
			COMMIT TRANSACTION;
			SET @cnt = @cnt + 1;
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION;
			END
			DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
			DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
			DECLARE @ErrorState INT = ERROR_STATE();
			PRINT 'An error occurred: ' + ERROR_MESSAGE();
			-- RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
			RETURN; -- Exit the stored procedure

				
		END CATCH
	END
	-- END OF 10 TRANSACTIONS
	
END
GO  

  


output>
current balance for a/c no = 11223344 = $2549.50
add balance = 917.99
updated balance for a/c no = 11223344 = $3467.49
payee id = 0
transaction amount = 917.99
transaction date = 2025-08-19
transaction status = 0
bank id = 1
account id = 1
last balance = 2549.50
current balance = 3467.49
ref code = 692D2
transaction type = 0
source id = 2


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

    }
}
