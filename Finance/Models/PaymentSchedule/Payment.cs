using System;

namespace Finance.Models.PaymentSchedule
{
    public class Payment
    {
        public DateTime PeriodStartDate { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public decimal InterestPayment { get; set; }
        public decimal PaymentAmount { get; set; }
        public double Balance { get; internal set; }
    }
}