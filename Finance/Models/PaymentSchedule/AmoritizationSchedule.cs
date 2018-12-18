using Finance.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Models.PaymentSchedule
{
    public class AmoritizationSchedule
    {
        public AmoritizationSchedule()
        {
            Payments = new List<Payment>();
        }

        public decimal Principal { get; set; }
        public decimal InterestRate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int NumberOfPayments { get; set; }

        public IEnumerable<Payment> Payments { get; }

        public IEnumerable<Payment> GenerateSchedule(double principal, double interestRate, int numberOfPayments)
        {
            var paymentAmount = CalculatePaymentAmountPerPeriod(principal, interestRate, numberOfPayments);
            return GenerateSchedule(principal, interestRate, paymentAmount);

        }

        public IEnumerable<Payment> GenerateSchedule(double principal, double interestRate, double paymentAmount)
        {
            double balance = principal;
            var payments = new List<Payment>();
            var startDate = DateTime.UtcNow;
            while (balance > 0)
            {
                double interestAmount = balance * ((interestRate / 12) / 100);
                if (paymentAmount < interestAmount)
                {
                    throw new InsufficientPaymentAmountException();
                }
                balance -= paymentAmount - interestAmount;
                var endDate = startDate.AddMonths(1);
                payments.Add(new Payment {
                    Balance = balance,
                    PaymentAmount = (decimal)paymentAmount,
                    InterestPayment = (decimal)interestAmount,
                    PeriodStartDate = startDate,
                    PeriodEndDate = endDate
                });
                startDate = endDate;
            }
            return payments;
        }

        public double CalculatePaymentAmountPerPeriod( double Principal, double InterestRate, int NumberOfPayments)
        {
            double paymentAmount = 0;
            double periodicInterestRate = (InterestRate / 12)/100;
            double numerator = periodicInterestRate * Math.Pow(1 + periodicInterestRate, NumberOfPayments);
            double denominator = Math.Pow(1 + periodicInterestRate, NumberOfPayments) - 1;
            paymentAmount = Principal * (
                 numerator /
                 denominator
            );

            return paymentAmount;
        }

        public int CalculateNumberOfPayments(double principal, double interestRate, double paymentAmount)
        {
            double balance = principal;
            int paymentCount = 0;
            while( balance > 0)
            {
                double interestAmount = balance * ((interestRate / 12) / 100);
                if( paymentAmount < interestAmount )
                {
                    throw new InsufficientPaymentAmountException();
                }
                balance -= paymentAmount - interestAmount;
                paymentCount++;
            }
            return paymentCount;
        }
    }
}
