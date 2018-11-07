using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.Data.EF;
using DEKL.CP.Infra.Data.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DEKL.CP.Infra.Service.EF
{
    public class ServiceEF<T> : IRepository<T> where T : EntityBase
    {
        private readonly IRepository<RepositoryEF> repository;

        public ServiceEF(DEKLCPDataContextEF ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<T> Get() => _ctx.Set<T>().ToList();

        public T Get(int id) => _ctx.Set<T>().Find(id);

        public void Add(T entity)
        {
            entity.DataCadastro = DateTime.Now;
            _ctx.Set<T>().Add(entity);
            Save();
        }

        public void Edit(T entity)
        {
            entity.DataAlteracao = DateTime.Now;
            _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            Save();
        }

        private void Save()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
