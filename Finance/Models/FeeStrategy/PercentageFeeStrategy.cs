namespace Finance.Models.FeeStrategy
{
    public class PercentageFeeStrategy
    {
        public decimal Amount { get; set; } = 0;
        
        public decimal Calculate(decimal Balance)
        {
            return Balance + Balance * (Amount / 100);
        }
    }
}
