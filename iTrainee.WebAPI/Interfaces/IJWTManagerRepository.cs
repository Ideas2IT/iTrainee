using iTrainee.Models;

namespace iTrainee.APIs.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(User user);

    }
}