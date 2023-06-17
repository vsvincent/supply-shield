using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<IUser> GetUserByEmail(string email);
        public Task<IUser> SetUser(IUser user);
    }
}
