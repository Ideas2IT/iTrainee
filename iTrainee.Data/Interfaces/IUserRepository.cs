using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetMentors();

    }
}
