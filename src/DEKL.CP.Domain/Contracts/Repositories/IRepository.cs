using DEKL.CP.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IRepository<T> : IDisposable where T : IEntityBase
    {
        IEnumerable<T> Get();
        IEnumerable<T> GetActives();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void DeleteLogical(T entity);
        void DeletePhysical(T entity);

    }
}
