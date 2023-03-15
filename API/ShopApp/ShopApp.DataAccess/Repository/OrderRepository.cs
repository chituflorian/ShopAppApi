using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;

namespace ShopApp.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ShopContext context) :
            base(context)
        {

        }

    }
}
