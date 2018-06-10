using System;

namespace DaGetV2Api.Domain
{
    public class UserBankAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public int BankAccountAccessId { get; set; }
        public BankAccountAccess BankAccountAccess { get; set; }
        public Guid UserId { get; set; }
    }
}
