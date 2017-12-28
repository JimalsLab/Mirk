using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mirk.DAL.IoC;
//using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq.Expressions;

namespace Mirk.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private MirkDBEntities dbContext;
        private readonly DbSet<TEntity> dbset;

        protected GenericRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected MirkDBEntities DataContext
        {
            get
            {

                if (dbContext == null)
                {
                    dbContext = DatabaseFactory.GetContext();
                }
                return dbContext;

            }
        }

        public virtual void Create(TEntity entity)
        {
            try
            {
                dbset.Add(entity);
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());  // TODO
            }
        }

        public virtual void CreateAll(IEnumerable<TEntity> entities)
        {
            try
            {
                dbset.AddRange(entities);
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual void Update(TEntity entity,int id)
        {
            try
            {
                TEntity status = dbContext.Set<TEntity>().Find(id);
                dbContext.Entry(status).CurrentValues.SetValues(entity);
                //dbset.Attach(entity);
                //dbContext.Entry(entity).State = EntityState.Modified;

            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }

        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                dbset.Remove(entity);
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                IQueryable<TEntity> objects = dbset.Where<TEntity>(where);
                foreach (TEntity obj in objects)
                {
                    dbset.Remove(obj);
                }
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
        }

        public virtual TEntity GetById(long id)
        {
            TEntity result = null;
            try
            {
                result = dbset.Find(id);
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
            return result;
        }

        public virtual TEntity GetById(String id)
        {
            TEntity result = null;
            try
            {
                result = dbset.Find(id);
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
            return result;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> results = null;
            try
            {
                results = dbset.ToList();
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
            return results;
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> results = null;
            try
            {
                results = dbset.Where(where).ToList();
            }
            catch (EntityException e)
            {
                throw new Exception(e.ToString());
            }
            return results;
        }

    }
}