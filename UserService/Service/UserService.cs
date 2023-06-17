using UserService.Models;
using UserService.Repository;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IUser> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<IUser> SetUser(IUser user)
        {
            return await _userRepository.SetUser(user);
        }
    }
}
