using System;
using System.Collections.Generic;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IRepository<T> : IDisposable where T : IEntityBase
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetActives();
        T Find(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void DeleteLogical(T entity);
        void DeletePhysical(T entity);

    }
}
