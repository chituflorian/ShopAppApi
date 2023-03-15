 using ShopApp.Core.Domain;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.DataInterface
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product FindBy(Expression<Func<Product, bool>> predicate);
    }  
}
