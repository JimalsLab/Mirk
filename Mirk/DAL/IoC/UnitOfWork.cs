using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Mirk.DAL.IoC
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory dbFactory;  //no need write nah ?
        private MirkDBEntities dbContext;

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected MirkDBEntities DataContext
        {
            get
            {

                if (dbContext == null)
                {
                    dbContext = dbFactory.GetContext();
                }
                return dbContext;

            }
        }

        public void Commit()
        {
            try
            {
                DataContext.Commit();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.ToString()); //faudrait faire des erreurs custom de db théoriquement mais pr le moment ca passe comme ca
            }
        }

        public void RollBack<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                try
                {
                    DbEntityEntry entityEntry = DataContext.Entry(entity);
                    switch (entityEntry.State)
                    {
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            entityEntry.Reload();
                            break;
                        case EntityState.Added:
                            entityEntry.State = EntityState.Unchanged;
                            break;
                    }
                }
                catch (DbUpdateException e)
                {
                    throw new Exception(e.ToString());  // saaaame
                }
            }
        }
    }
}