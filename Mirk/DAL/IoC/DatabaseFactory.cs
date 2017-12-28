using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.IoC
{
    public class DatabaseFactory : IDatabaseFactory, IDisposable
    {
        private MirkDBEntities context;

        public MirkDBEntities GetContext()
        {
            if (context == null)
            {
                context = new MirkDBEntities();
            }
            return context;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}