using DaGetCore.Service.Dto;

namespace DaGetCore.Service
{
    public interface IBankAccountService
    {
        BankAccountDto GetById(string userName, int id);
        BankAccountDto Create(string userName, BankAccountDto toCreate);
    }
}
