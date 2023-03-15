using ShopApp.DataAccess.DataInterface;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShopContext context;

        public Repository(ShopContext context)
        {
            this.context = context;
        }

        public virtual T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }
        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public virtual void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public virtual bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

       
    }
}
