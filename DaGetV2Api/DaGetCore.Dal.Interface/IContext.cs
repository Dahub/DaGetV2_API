using System;

namespace DaGetCore.Dal.Interface
{
    public interface IContext : IDisposable
    {
        void Commit();
        void CommitAsync();
    }
}
