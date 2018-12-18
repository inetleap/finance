namespace Finance.Models.FeeStrategy
{
    public class RateStep
    {
        public decimal Rate { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal? UpperLimit { get; set; }
        
    }
}
