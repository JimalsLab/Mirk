using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.Services
{
    public interface IGenericService<TBean, TEntity> where TBean : class, new() where TEntity : class, new()
    {
        int? Create(TBean bean);
        IEnumerable<TBean> CreateAll(ICollection<TBean> beans);
        TBean GetById(long id);
        TBean GetById(String id);
        IEnumerable<TBean> GetAll();
        IEnumerable<TBean> GetMany(Expression<Func<TEntity, bool>> where);
        void Update(TBean bean,int id);
        void Delete(TBean bean);
        void Delete(Expression<Func<TEntity, bool>> where);

        void Commit(TEntity entity);
        void RollBack(TEntity entity);
    }
}
