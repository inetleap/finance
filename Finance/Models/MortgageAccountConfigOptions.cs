using Finance.Enums;
using Finance.Interfaces;

namespace Finance.Models
{
    public class MortgageAccountConfigOptions: AccountConfigOptions, IAccountConfigOptions
    {
        private static readonly AccountType _accountType = AccountType.MORTGAGE;
        public string Property { get; set; }
    }
}
