using System.Collections.Generic;

namespace DaGetCore.Domain
{
    public class BankAccountAccess
    {
        public int Id { get; set; }
        public string Wording { get; set; }

        public ICollection<UserBankAccount> UsersBankAccounts { get; set; }
    }
}
