using Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Database
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public Repository(IDbContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).Aggregate(string.Empty,
                    (current, validationError) => current + ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                var e = _context.Entry(entity).Entity;
                _context.Entry(e).State = EntityState.Added;
                e = entity;
                _context.Entry(e).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) => validationErrors.ValidationErrors.Aggregate(
                    current1, (current, validationError) => current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void UpdateByID(object[] id, T entity)
        {
            var e = Entities.Find(id);
            _context.Entry(e).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _context.Entry(entity).State = EntityState.Deleted;
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).Aggregate(
                    string.Empty, (current, validationError) => current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void DeleteById(object[] id)
        {
            try
            {
                var e = Entities.Find(id);
                Entities.Remove(e);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public virtual IQueryable<T> Table => Entities;

        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
