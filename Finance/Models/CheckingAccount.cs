using Finance.Enums;

namespace Finance.Models
{
    public class CheckingAccount : Account
    {
        public static readonly AccountType AccountType = AccountType.CHECKING;

        public CheckingAccount ( CheckingAccountConfigOptions cfg)
        {
            Id = cfg.Id;
            Name = cfg.Name;
            AccountOwner = cfg.AccountOwner;
        }
        public string AccountOwner { get; set; }
    }
}
