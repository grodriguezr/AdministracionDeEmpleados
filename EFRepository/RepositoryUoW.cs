using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    public class RepositoryUoW : Repository, IRepository, IRepositoryUoW, IDisposable
    {
        //protected DbContext Context;
        public RepositoryUoW(DbContext context, bool autoDetectChanges = false, bool proxyCreationEnabled = false):
            base(context, autoDetectChanges, proxyCreationEnabled)
        {
            this.Context = context;
            this.Context.Configuration.AutoDetectChangesEnabled = autoDetectChanges;
            this.Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }

        
        protected override int TrySaveChanges()//sobreescibo el método
        {
            return 0;
        }

        //https://www.facebook.com/La-Contadora-de-Historias-1315653701809566/?fref=ts

        int IRepositoryUoW.Save()
        {
            int Result = 0;
            try
            {
                Result = Context.SaveChanges();

            }
            catch (Exception e)
            {

                throw (e);
            }
            return Result;
        }
    }
}
