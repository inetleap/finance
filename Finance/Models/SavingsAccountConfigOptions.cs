using Finance.Enums;
using Finance.Interfaces;

namespace Finance.Models
{
    public class SavingsAccountConfigOptions : AccountConfigOptions, IAccountConfigOptions
    {
        private static readonly AccountType _accountType = AccountType.SAVINGS;
    }
}
