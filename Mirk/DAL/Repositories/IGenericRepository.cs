using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void CreateAll(IEnumerable<TEntity> entities);
        TEntity GetById(long id);
        TEntity GetById(String id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        void Update(TEntity entity,int id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
    }
}
