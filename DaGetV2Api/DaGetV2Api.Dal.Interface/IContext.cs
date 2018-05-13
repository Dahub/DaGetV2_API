using System;

namespace DaGetV2Api.Dal.Interface
{
    public interface IContext : IDisposable
    {
        void Commit();
        void CommitAsync();
    }
}
