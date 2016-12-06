using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepository<T> where T: class
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void UpdateByID(object[] id, T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        void DeleteById(object[] id);
        void Dispose();
    }
}
