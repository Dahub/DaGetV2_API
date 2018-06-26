using System.Collections.Generic;

namespace DaGetCore.Domain
{
    public class BankAccountAccess : DomainObjectBase
    {
        public string Wording { get; set; }

        public ICollection<UserBankAccount> UsersBankAccounts { get; set; }
    }
}
