using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;

namespace ShopApp.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShopContext context)
            : base(context)
        {
        }

        public IEnumerable<User> GetUsers(bool includesOrders)
        {
            return GetAll();
        }
    }
}
