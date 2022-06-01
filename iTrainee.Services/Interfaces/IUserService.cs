using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);

        IEnumerable<User> GetUsers(string role);

        IEnumerable<User> GetAssignedTrainees(int batchId);

        bool SaveUser(User user);

        bool DeleteUser(int id);

        User GetUserByUserName(string userName, string password);

        bool UpdateUser(User user);
        string[] GetAssignedBatchIds(int userId);
    }
}
