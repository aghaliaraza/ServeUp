using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServeUp.Data
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task Insert(TEntity entity); 
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        IList<TEntity> GetAll();
        TEntity GetById(Guid id);
    }
}
