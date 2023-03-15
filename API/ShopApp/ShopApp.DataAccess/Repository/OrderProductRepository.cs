using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;

namespace ShopApp.DataAccess.Repository
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(ShopContext context)
            : base(context)
        {
        }
    }
}
