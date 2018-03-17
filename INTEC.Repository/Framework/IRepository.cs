using System;
using INTEC.Data.Infaestructure;
using System.Linq;
using System.Linq.Expressions;

namespace INTEC.Repository.Framework
{
    public interface IRepository<T> where T : class
    {
        DataResult GetAll();
        DataResult GetAll(Expression<Func<T, bool>> specification);
        DataResult GetById(int id);
        DataResult Insert(T entity);
        DataResult Update(T entity);
        DataResult Delete(T entity);
        DataResult SaveChanges();
    }
}
