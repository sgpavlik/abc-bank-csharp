using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestCalculators
{
    interface IInterestCalculator
    {
        double InterestEarnedForThePeriod(DateTime startDate, DateTime endDate, double amount);
    }
}
