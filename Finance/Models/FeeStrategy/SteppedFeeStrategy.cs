using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.Models.FeeStrategy
{
    public class SteppedFeeStrategy
    {
        public decimal Amount { get; set; } = 0;
        public IEnumerable<RateStep> RateSteps { get; set; }

        public decimal Calculate(decimal balance)
        {
            foreach( var step in RateSteps)
            {
                
                if (balance < step.UpperLimit) {
                    return balance + ( balance * (step.Rate/100) );
                } 
            }

            var lastStep = RateSteps.ToList().Last();
            return balance + ( balance * (lastStep.Rate/100) );
            
        }
    }
}
