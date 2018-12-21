using DEKL.CP.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IRepository<T> : IDisposable where T : IEntityBase
    {
        IEnumerable<T> All { get; }
        IEnumerable<T> Actives { get; }
        T Find(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void DeleteLogical(T entity);
        void DeletePhysical(T entity);

    }
}
