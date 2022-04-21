using iTrainee.Models;

namespace iTrainee.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
    }
}
