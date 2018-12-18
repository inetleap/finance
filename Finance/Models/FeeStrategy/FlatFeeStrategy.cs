using Finance.Enums;

namespace Finance.Models.FeeStrategy
{
    public class FlatFeeStrategy
    {
        public decimal Amount { get; set; } = 0;
        public FeeCalculationPeriod CalculationPeriod { get; set; }

        public decimal Calculate(decimal balance)
        {
            return balance + Amount;
        }
    }
}
