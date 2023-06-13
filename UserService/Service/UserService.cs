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
        public async Task<IUser> SetUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await _userRepository.SetUser(user);
        }
    }
}
