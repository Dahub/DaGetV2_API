using DaGetCore.Service.Dto;
using DaGetCore.Service.Tools;

namespace DaGetCore.Service
{
    public class BankAccountService : ServiceBase, IBankAccountService
    {
        public BankAccountDto Create(string userName, BankAccountDto toCreate)
        {
            throw new DaGetServiceException("test");
        }

        public BankAccountDto GetById(string userName, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
