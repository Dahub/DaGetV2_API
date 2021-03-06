﻿using DaGetCore.Domain;
using System;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {       
        IEnumerable<BankAccount> GetAllByIdUser(Guid userId);
        BankAccount GetByIdUserAndId(Guid userId, int id);
    }
}
