using Finance.Enums;

namespace Finance.Models
{
    public class MortgageAccount: Account
    {
        public static readonly AccountType AccountType = AccountType.MORTGAGE;
        public MortgageAccount( MortgageAccountConfigOptions cfg)
        {
            Id = cfg.Id;
            Name = cfg.Name;
            Property = cfg.Property;
        }
        public string Property { get; set; }
    }
}
