using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Users AuthenticateUser(Users users);
        string GenerateToken(Users users);
    }
}
