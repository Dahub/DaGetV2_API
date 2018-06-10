using DaGetV2Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaGetV2Api.Service
{
    public class BankAccountService : ServiceBase
    {
        public BankAccountDto CreateNewBankAccount(BankAccountDto bankAccount, string userName)
        {
            return bankAccount;
        }
    }
}
