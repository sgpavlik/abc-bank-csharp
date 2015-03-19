using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.InterestCalculators;

namespace abc_bank
{
    public enum AccountType
    {
        CHECKING = 0,
        SAVINGS = 1,
        MAXI_SAVINGS = 2
    }

    public class Account
    {
        private readonly AccountType accountType;
        private readonly IInterestCalculator interestCalculator;

        public List<Transaction> transactions;

        public Account(AccountType accountType) 
        {
            this.accountType = accountType;
            switch (accountType)
            {
                case AccountType.CHECKING:
                    interestCalculator = new CheckingInterestCalculator();
                    break;
                case AccountType.SAVINGS:
                    interestCalculator = new SavingsInterestCalculator();
                    break;
                case AccountType.MAXI_SAVINGS:
                    interestCalculator = new MaxiSavingsInterestCalculator();
                    break;
            }

            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Deposit(double amount, DateTime tranDateTime)
        {
            if (amount <= 0){
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount, tranDateTime));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                double balance = sumTransactions();
                if (amount > balance)
                    throw new Exception("Insufficient funds");

                transactions.Add(new Transaction(-amount));
            }
        }

        public void Withdraw(double amount, DateTime tranDateTime)
        {
            if (amount <= 0){
                throw new ArgumentException("amount must be greater than zero");
            } else {
                double balance = sumTransactions();
                if (amount > balance)
                    throw new Exception("Insufficient funds");

                transactions.Add(new Transaction(-amount, tranDateTime));
            }
        }

        public double InterestEarned() 
        {
            double currentAmount = 0.0;
            double currentInterest = 0.0;

            for(int i = 0; i < transactions.Count - 1; i++)
            {
                currentAmount = GetAmountAfterTransaction(i);
                currentInterest += 
                    InterestEarnedForThePeriod(transactions[i].transactionDate, transactions[i + 1].transactionDate, currentAmount);
            }

            currentAmount = GetAmountAfterTransaction(transactions.Count - 1);
            currentInterest += 
                InterestEarnedForThePeriod(transactions[transactions.Count - 1].transactionDate, DateTime.Now.Date, currentAmount);

            return currentInterest;
        }

        private double GetAmountAfterTransaction(int n)
        {
            double amount = 0.0;

            for (int i = 0; i <= n; i++)
                amount += transactions[i].amount;

            return amount;
        }

        private double InterestEarnedForThePeriod(DateTime startDate, DateTime endDate, double amount)
        {
            return interestCalculator.InterestEarnedForThePeriod(startDate, endDate, amount);
        }

        public double sumTransactions() 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public AccountType GetAccountType() 
        {
            return accountType;
        }
    }
}
