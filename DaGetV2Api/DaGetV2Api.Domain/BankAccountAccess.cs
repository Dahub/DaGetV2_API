using System.Collections.Generic;

namespace DaGetV2Api.Domain
{
    public class BankAccountAccess
    {
        public int Id { get; set; }
        public string Wording { get; set; }

        public ICollection<UserBankAccount> UsersBankAccounts { get; set; }
    }
}
