 using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ShopContext context)
            : base(context)
        {
        }

        public Product FindBy(Expression<Func<Product, bool>> predicate)
        {
            return (Product)context.Set<Product>().Where(predicate);
        }

    }
}
