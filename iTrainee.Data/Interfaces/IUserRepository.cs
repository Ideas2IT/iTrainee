using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers(string role);

        bool InsertUser(User user);

        bool DeleteUser(int id);
        User GetUserByUserName(string userName);
    }
}
