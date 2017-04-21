using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Diagnostics;

namespace EFRepository
{
    public class Repository : IRepository, IDisposable
    {
        protected DbContext Context;
        public Repository(DbContext context, bool autoDetectChanges = false, bool proxyCreationEnabled = false)
        {
            this.Context = context;
            this.Context.Configuration.AutoDetectChangesEnabled = autoDetectChanges;
            this.Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }

        //#########################################################################
        public IQueryable<TEntity> FindEntitySet<TEntity>(string related) where TEntity : class
        {
            return Context.Set<TEntity>().Include(related);
        }
        //#########################################################################

        public TEntity Create<TEntity>(TEntity newEntity) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = Context.Set<TEntity>().Add(newEntity);
                Context.Entry<TEntity>(newEntity).State = EntityState.Added;
                TrySaveChanges();
            }
            catch (Exception e)
            {
                throw(e);
            }
            return Result;
        }

        public bool Delete<TEntity>(TEntity deletedEntity) where TEntity : class
        {
            bool Result = false;
            try
            {
                Context.Set<TEntity>().Attach(deletedEntity);
                Context.Set<TEntity>().Remove(deletedEntity);
                
                Result = TrySaveChanges() > 0;
            }
            catch (Exception e)
            {

                throw(e);
            }

            return Result;
        }

        public void Dispose()
        {
            if(Context != null)
            {
                Context.Dispose();
            }
        }
       
        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = Context.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception e)
            {

                throw(e);
            }
            return Result;
        }

        //###############################################################
        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria, string related) where TEntity : class
        {
            return Context.Set<TEntity>().Include(related).FirstOrDefault(criteria);

        }
        //###############################################################


        public IEnumerable<TEntity> FindEntitySet<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Result = null;
            try
            {
                Result = Context.Set<TEntity>().Where(criteria).ToList();
                
            }
            catch (Exception e)
            {

                throw(e);
            }
            return Result;
        }

       
        public bool Update<TEntity>(TEntity updatedEntity) where TEntity : class
        {
            bool Result = false;
            try
            {
                Context.Set<TEntity>().Attach(updatedEntity);
                Context.Entry<TEntity>(updatedEntity).State = EntityState.Modified;
                Result = TrySaveChanges() > 0;
            }
            catch (Exception e)
            {

                throw(e);
            }
            return Result;
        }

        protected virtual int TrySaveChanges()//virtual permite que el método pueda ser sobreescrito
        {
            Debug.WriteLine("Cambios guardados");
            return Context.SaveChanges();//retorna un número entero que serán la cantidad de cambios realizados

        }
    


        //crear un método de extensión
        public IQueryable<TEntity> FindEntitySet<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
