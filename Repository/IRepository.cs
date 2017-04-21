using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository : IDisposable
    {
        //definción de la interfaz IRepository la cual estará compuesta por métodos genéricos que podrán trabajar con cualquier tipo de entidad que se les pase como parámetro
        TEntity Create<TEntity>(TEntity newEntity) where TEntity : class;
        bool Update<TEntity>(TEntity updatedEntity) where TEntity : class;
        bool Delete<TEntity>(TEntity deletedEntity) where TEntity : class;
        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        IEnumerable<TEntity> FindEntitySet<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //sobrecargas
        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria, string related) where TEntity : class;
        IQueryable<TEntity> FindEntitySet<TEntity>(string related) where TEntity : class;
        
        //prueba
        IQueryable<TEntity> FindEntitySet<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

    }
}
