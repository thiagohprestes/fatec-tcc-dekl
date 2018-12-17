using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DEKL.CP.Infra.Data.EF.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _ctx;
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;

        public RepositoryEF(DbContext ctx) => _ctx = ctx;

        public IEnumerable<T> GetAll() => Entities.ToList();

        public IEnumerable<T> GetActives() => Entities.Where(e => e.Active).ToList();

        public T Find(int id) => Entities.Find(id);

        public IEnumerable<T> Find(Func<T, bool> predicate) => Entities.Where(predicate);

        public void Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                entity.AddedDate = DateTime.Now;
                Entities.Add(entity);
                _ctx.SaveChanges();
            }

            catch (DbEntityValidationException dbEx)
            {
                dbEx.EntityValidationErrors.ToList().ForEach(eve =>
                {
                    eve.ValidationErrors.ToList().ForEach(vr =>
                    {
                        _errorMessage += $"Property: {vr.PropertyName} Error: {vr.ErrorMessage}" + Environment.NewLine;
                    });
                });

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                entity.ModifiedDate = DateTime.Now;
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                dbEx.EntityValidationErrors.ToList().ForEach(eve =>
                {
                    eve.ValidationErrors.ToList().ForEach(vr =>
                    {
                        _errorMessage += $"Property: {vr.PropertyName} Error: {vr.ErrorMessage}" + Environment.NewLine;
                    });
                });

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void DeleteLogical(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                entity.Active = false;
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                dbEx.EntityValidationErrors.ToList().ForEach(eve =>
                {
                    eve.ValidationErrors.ToList().ForEach(vr =>
                    {
                        _errorMessage += $"Property: {vr.PropertyName} Error: {vr.ErrorMessage}" + Environment.NewLine;
                    });
                });

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void DeletePhysical(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                dbEx.EntityValidationErrors.ToList().ForEach(eve =>
                {
                    eve.ValidationErrors.ToList().ForEach(vr =>
                    {
                        _errorMessage += $"Property: {vr.PropertyName} Error: {vr.ErrorMessage}" + Environment.NewLine;
                    });
                });

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public virtual IQueryable<T> Table => Entities;

        private IDbSet<T> Entities => _entities ?? (_entities = _ctx.Set<T>());
    }
}
