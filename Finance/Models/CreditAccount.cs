using Finance.Enums;
using Finance.Interfaces;

namespace Finance.Models
{
    public class CreditAccount : Account
    {
        public static readonly AccountType AccountType = AccountType.CREDIT;
        public CreditAccount():base() { }
        public CreditAccount( CreditAccountConfigOptions cfg)
        {
            Id = cfg.Id;
            Name = cfg.Name;
        }
    }
}
