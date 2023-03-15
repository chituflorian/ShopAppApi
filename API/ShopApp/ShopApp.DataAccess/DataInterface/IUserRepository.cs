using ShopApp.Core.Domain;

namespace ShopApp.DataAccess.DataInterface
{
    public interface IUserRepository : IRepository<User>
    {
        public IEnumerable<User> GetUsers(bool includesOrders);
    }
}
