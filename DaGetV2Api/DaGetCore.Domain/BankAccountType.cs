using System.Collections.Generic;

namespace DaGetCore.Domain
{
    public class BankAccountType : DomainObjectBase
    {
        public string Wording { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
