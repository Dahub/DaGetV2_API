using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal abstract class RepositoryBase<T> : IRepository<T> where T : DomainObjectBase
    {
        public IContext Context { get; set; }

        public void Add(T toAdd)
        {
            ((DbContext)Context).Set<T>().Add(toAdd);
        }

        public void Delete(T toDelete)
        {
            ((DbContext)Context).Set<T>().Attach(toDelete);
            ((DbContext)Context).Entry(toDelete).State = EntityState.Deleted;
        }

        public IEnumerable<T> GetAll()
        {
            return ((DaGetContext)Context).Set<T>();
        }

        public T GetById(int id)
        {
            return ((DaGetContext)Context).Set<T>().Where(t => t.Id.Equals(id)).FirstOrDefault();
        }

        public void Update(T toUpdate)
        {
            ((DbContext)Context).Set<T>().Attach(toUpdate);
            ((DbContext)Context).Entry(toUpdate).State = EntityState.Modified;
        }
    }
}
