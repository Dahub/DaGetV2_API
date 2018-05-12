using System.Collections.Generic;

namespace DaGetV2Api.Domain
{
    public class BankAccountType
    {
        public int Id { get; set; }
        public string Wording { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
