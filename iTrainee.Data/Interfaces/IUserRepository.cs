using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetMentors();

        bool InsertUser(User user);

        bool DeleteUser(int id);
        User GetUserByUserName(string userName);
    }
}
