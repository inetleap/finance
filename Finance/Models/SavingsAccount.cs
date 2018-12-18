using Finance.Enums;

namespace Finance.Models
{
    public class SavingsAccount: Account
    {
        public static readonly AccountType AccountType = AccountType.SAVINGS;
        public SavingsAccount( SavingsAccountConfigOptions config) : base() {
            Id = config.Id;
            Name = config.Name;
        }
    }
}
