using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestCalculators
{
    class CheckingInterestCalculator : IInterestCalculator
    {
        double yearlyInterest = 0.001;

        #region IInterestCalculator Members

        public double InterestEarnedForThePeriod(DateTime startDate, DateTime endDate, double amount)
        {
            TimeSpan diff = endDate - startDate;
            int numberOfDays = diff.Days;

            double daylyInterest = yearlyInterest / 365.0;

            double interest = amount * (Math.Pow(1.0 + daylyInterest, numberOfDays) - 1.0);

            return interest;
        }

        #endregion
    }
}
