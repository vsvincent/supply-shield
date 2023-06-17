using UserService.Models;

namespace UserService.Service
{
    public interface IUserService
    {
        public Task<IUser> GetUser(string email);
        public Task<IUser> SetUser(IUser user);
    }
}
