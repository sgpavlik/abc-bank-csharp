using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-4;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            DateTime depositDate = DateTime.Now.Date.Subtract(new TimeSpan(365, 0, 0, 0));
            checkingAccount.Deposit(100.0, depositDate);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            DateTime depositDate = DateTime.Now.Date.Subtract(new TimeSpan(365, 0, 0, 0));
            savingsAccount.Deposit(1500.0, depositDate);

            Assert.AreEqual(2.0015, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            DateTime depositDate = DateTime.Now.Date.Subtract(new TimeSpan(365, 0, 0, 0));
            maxiSavingsAccount.Deposit(3000.0, depositDate);

            Assert.AreEqual(176.624, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
