using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        public Task<IUser> SetUser(User user);
    }
}
