using System;
using System.Collections.Generic;

namespace Finance.Models.FeeStrategy
{
    public class MarginalFeeStrategy
    {
        public decimal Amount { get; set; } = 0;
        public IEnumerable<RateStep> RateSteps { get; set; }

        public decimal Calculate(decimal balance)
        {
            decimal total = 0;
            foreach (var step in RateSteps)
            {

                if( balance > step.LowerLimit)
                {
                    if( step.UpperLimit == null || balance <= step.UpperLimit)
                    {
                        total += (balance - step.LowerLimit) * (step.Rate / 100);
                    } else
                    {
                        total += ((decimal)step.UpperLimit - step.LowerLimit) * (step.Rate / 100 );
                    }
                }
            
            }
            return total;
        }
    }
}
