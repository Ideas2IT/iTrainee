using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
