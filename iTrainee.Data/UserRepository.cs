using iTrainee.Data.Interfaces;
using iTrainee.Models;

namespace iTrainee.Data
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(int id)
        {
            return new User();
        }
    }
}
