using System;
using Xunit;
using Finance.Models;
using Finance.Factories;
using Finance.Enums;
using Finance.Interfaces;
using System.Collections.Generic;
using Finance.Models.FeeStrategy;
using Finance.Models.PaymentSchedule;
using System.Linq;

namespace FinanceTest
{
    public class Finance
    {
        [Fact]
        public void AccountFactoryCreatesCorrectAccountTypes()
        {
            IEnumerable<IAccountConfigOptions> cfgOptions = new List<IAccountConfigOptions>() {
                new CreditAccountConfigOptions { Id = "alalalalsls", Name = "CreditAccount 1" },
                new MortgageAccountConfigOptions { Id = "dkdlassadlkj", Name = "MortgageAccount 1", Property="Some Property" },
                new CheckingAccountConfigOptions(),
                new SavingsAccountConfigOptions()
            };
            List<Account> accounts = new List<Account>();
            foreach( var cfg in cfgOptions)
            {
                accounts.Add(AccountFactory.GetAccount(cfg));
            }
            Assert.IsType<CreditAccount>(accounts[0]);
            Assert.IsType<MortgageAccount>(accounts[1]);
            Assert.IsType<CheckingAccount>(accounts[2]);
            Assert.IsType<SavingsAccount>(accounts[3]);
        }

        [Fact]
        public void CanCreateCreditAccount()
        {
            IAccountConfigOptions accountConfigOptions = new CreditAccountConfigOptions
            {
                Id = "Test Credit Id",
                Name = "Test Credit Account"
            };
            var creditAccount = AccountFactory.GetAccount(accountConfigOptions);
            Assert.NotNull(creditAccount);
            Assert.Equal("Test Credit Id",creditAccount.Id);
        }

        [Theory]
        [InlineData(100, 10, 110)]
        [InlineData(300.24, 100.26, 400.50)]
        public void CalculateFlatFees(decimal Balance, decimal FeeAmount, decimal Expected) {
            var strategy = new FlatFeeStrategy() { Amount = FeeAmount, CalculationPeriod=FeeCalculationPeriod.OneTime };
            var Result = strategy.Calculate(Balance);
            Assert.Equal(Result, Expected);

        }

        [Theory]
        [InlineData(100, 10, 110.00)]
        [InlineData(300, 10, 330.00)]
        [InlineData(25, 20, 30.00)]
        public void CalculatePercentageFees(decimal Balance, decimal FeeAmount, decimal Expected) {
            var strategy = new PercentageFeeStrategy() { Amount = FeeAmount };
            var Result = strategy.Calculate(Balance);
            Assert.Equal(Expected, Result );

        }

        [Theory]
        [InlineData(100, 125.00)]
        [InlineData(300, 375.00)]
        [InlineData(1000, 1150.00)]
        public void CalculateSteppedRateFees(decimal Balance, decimal Expected)
        {
            var strategy = new SteppedFeeStrategy() {
                Amount = Balance,
                RateSteps = new List<RateStep> {
                    new RateStep { Rate=30M, LowerLimit=0, UpperLimit= 100.00M },
                    new RateStep { Rate=25M, LowerLimit=100M, UpperLimit= 500.00M },
                    new RateStep { Rate=15M, LowerLimit=500M, UpperLimit= null }
                }
            };
            var Result = strategy.Calculate(Balance);
            Assert.Equal(Result, Expected);

        }

        [Theory]
        [InlineData(100, 10.00)]
        [InlineData(300, 50.00)]
        [InlineData(1000, 240.00)]
        public void CalculateMarginalRateFees(decimal Balance, decimal Expected)
        {
            var strategy = new MarginalFeeStrategy()
            {
                Amount = Balance,
                RateSteps = new List<RateStep> {
                    new RateStep { Rate=10M, LowerLimit=0, UpperLimit= 100.00M },
                    new RateStep { Rate=20M, LowerLimit=100M, UpperLimit= 500.00M },
                    new RateStep { Rate=30M, LowerLimit=500M, UpperLimit= null }
                }
            };
            var Result = strategy.Calculate(Balance);
            Assert.Equal( Expected, Result);

        }

        [Theory]
        [InlineData(19050, 1905)]
        [InlineData(77400, 8906.88)]
        [InlineData(100000, 13878.66)]
        public void Calculate2018MarginalTaxRateFees(decimal Balance, decimal Expected)
        {
            var strategy = new MarginalFeeStrategy()
            {
                Amount = Balance,
                RateSteps = new List<RateStep> {
                    new RateStep { Rate=10M, LowerLimit=0, UpperLimit= 19050M }, // 1905
                    new RateStep { Rate=12M, LowerLimit=19051.00M, UpperLimit= 77400.00M }, // 7001.88
                    new RateStep { Rate=22M, LowerLimit=77401M, UpperLimit= 165000M },
                    new RateStep { Rate=24M, LowerLimit=165001M, UpperLimit= 315000M },
                    new RateStep { Rate=32M, LowerLimit=315001M, UpperLimit= 400000M },
                    new RateStep { Rate=32M, LowerLimit=400001M, UpperLimit= 600000M },
                    new RateStep { Rate=32M, LowerLimit=600001M, UpperLimit= null }
                }
            };
            var Result = strategy.Calculate(Balance);
            Assert.Equal(Expected, Result);

        }

        [Theory]
        [InlineData(20000.00,7.5,60,400.76)]
        public void CalculatePaymentAmountPerPeriod(double Principal, double InterestRate, int numberOfPayments, double Expected)
        {
            var amSchedule = new AmoritizationSchedule();
            var paymentAmount = amSchedule.CalculatePaymentAmountPerPeriod(Principal, InterestRate, numberOfPayments);
            Assert.Equal(Expected, Math.Round(paymentAmount,2));
        }

        [Theory]
        [InlineData(20000.00, 7.5, 400.76, 60)]
        public void CalculateNumberOfPaymentsForGivenAmount(double Principal, double InterestRate, double paymentAmount, double Expected)
        {
            var amSchedule = new AmoritizationSchedule();
            var numberOfPayments = amSchedule.CalculateNumberOfPayments(Principal, InterestRate, paymentAmount);
            Assert.Equal(Expected, numberOfPayments);
        }

        [Theory]
        [InlineData(20000.00, 7.5, 400.76, 60)]
        public void GeneratePaymentSchedule(double Principal, double InterestRate, double paymentAmount, double Expected)
        {
            var amSchedule = new AmoritizationSchedule();
            var numberOfPayments = amSchedule.GenerateSchedule(Principal, InterestRate, paymentAmount);
            Assert.Equal(Expected, numberOfPayments.ToList().Count());
        }
    }
}
