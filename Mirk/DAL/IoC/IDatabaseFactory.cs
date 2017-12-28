using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.IoC
{
    public interface IDatabaseFactory : IDisposable
    {
        MirkDBEntities GetContext();
    }
}
