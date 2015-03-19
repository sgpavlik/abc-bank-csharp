using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestCalculators
{
    class SavingsInterestCalculator : IInterestCalculator
    {
        double yearlyInterest1 = 0.001;
        double yearlyInterest2 = 0.002;

        #region IInterestCalculator Members

        public double InterestEarnedForThePeriod(DateTime startDate, DateTime endDate, double amount)
        {
            TimeSpan diff = endDate - startDate;
            int numberOfDays = diff.Days;

            double daylyInterest1 = yearlyInterest1 / 365.0;
            double daylyInterest2 = yearlyInterest2 / 365.0;

            double interest1 = 0;
            double interest2 = 0;

            if (amount <= 1000)
            {
                interest1 = amount * (Math.Pow(1.0 + daylyInterest1, numberOfDays) - 1.0);
                return interest1;
            }
            else
            {
                interest1 = 1000.0 * (Math.Pow(1.0 + daylyInterest1, numberOfDays) - 1.0);
                interest2 = (amount - 1000.0) * (Math.Pow(1.0 + daylyInterest2, numberOfDays) - 1.0);
                return interest1 + interest2;
            }
        }

        #endregion
    }
}
