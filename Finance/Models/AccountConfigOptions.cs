using Finance.Enums;

namespace Finance.Models
{
    public abstract class AccountConfigOptions
    {
        private static readonly AccountType _accountType;
        public AccountType AccountType {
            get {
                var field = GetType().GetField("_accountType",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.Static | 
                    System.Reflection.BindingFlags.NonPublic);

                return (AccountType)field.GetValue(this);
            }
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
