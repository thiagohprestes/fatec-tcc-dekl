using DEKL.CP.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}
