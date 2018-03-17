using System;
using System.Linq.Expressions;
using INTEC.Data;
using INTEC.Data.Infaestructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace INTEC.Repository.Framework
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> dbSet;

        public Repository(ApplicationContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException("Debes especificar un Contexto a tu Aplicacion");
            }

            this.context = context;
            dbSet = context.Set<T>();
        }

        public DataResult GetAll()
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;
                result.Data = dbSet.ToList();

                //SELECT * FROM TABLA
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult GetAll(Expression<Func<T, bool>> specification)
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;
                result.Data = dbSet.Where(specification).ToList();

                //SELECT * FROM TABLA WHERE Nombre = 'Omar Frometa'
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult GetById(int id)
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;
                result.Data = dbSet.Find(id);

                //SELECT * FROM TABLA WHERE Id = @Id
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult Insert(T entity)
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;

                dbSet.Add(entity);
                context.SaveChanges();

                result.Data = entity;

                //INSERT INTO TABLE(Id, UserName, Password) VALUES (1, OFrometa, miClave)
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult SaveChanges()
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;
                result.Data = context.SaveChanges();

               //SAVE ENTITY STATE
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult Update(T entity)
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;

                context.SaveChanges();

                result.Data = entity;

                //UDATE TABLE SET Campo1 = OFrometa WHERE Id = @Id
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }

        public DataResult Delete(T entity)
        {
            DataResult result = new DataResult();

            try
            {
                result.Success = true;

                dbSet.Remove(entity);
                context.SaveChanges();

                result.Data = entity;

                //DELETE FROM TABLE WHERE Id = @Id
            }
            catch (Exception ex)
            {
                result.LogError(ex);
            }

            return result;
        }
    }
}
