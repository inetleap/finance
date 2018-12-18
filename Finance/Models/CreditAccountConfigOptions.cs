using Finance.Enums;
using Finance.Interfaces;

namespace Finance.Models
{
    public class CreditAccountConfigOptions : AccountConfigOptions, IAccountConfigOptions
    {
        private static readonly AccountType _accountType = AccountType.CREDIT;
    }
}
