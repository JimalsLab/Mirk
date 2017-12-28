using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.IoC
{
    public interface IUnitOfWork
    {
        void Commit();
        void RollBack<TEntity>(TEntity entity) where TEntity : class;
    }
}
