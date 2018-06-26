using DaGetCore.Domain;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IRepository<T> where T : DomainObjectBase
    {
        IContext Context { get; set; }
        void Add(T toAdd);
        void Update(T toUpdate);
        void Delete(T toDelete);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
