using Finance.Enums;
using Finance.Interfaces;

namespace Finance.Models
{
    public class CheckingAccountConfigOptions : AccountConfigOptions, IAccountConfigOptions
    {
        private static readonly AccountType _accountType = AccountType.CHECKING;
        public string AccountOwner { get; set; }
    }
}
