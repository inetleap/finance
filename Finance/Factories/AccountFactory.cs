using Finance.Enums;
using Finance.Exceptions;
using Finance.Interfaces;
using Finance.Models;

namespace Finance.Factories
{
    public class AccountFactory
    {
        public AccountFactory()
        {
        }

        public static Account GetAccount( IAccountConfigOptions config ) {
            Account accnt;
            switch (config.AccountType)
            {
                case AccountType.CHECKING:
                    accnt = new CheckingAccount(config as CheckingAccountConfigOptions);
                    break;
                case AccountType.SAVINGS:
                    accnt = new SavingsAccount(config as SavingsAccountConfigOptions);
                    break;
                case AccountType.CREDIT:
                    accnt = new CreditAccount(config as CreditAccountConfigOptions);
                    break;
                case AccountType.MORTGAGE:
                    accnt = new MortgageAccount(config as MortgageAccountConfigOptions);
                    break;
                default:
                    throw new InvalidAccountTypeException("Unsupported Account Type");
            }
            return accnt;
        }
    }
}
