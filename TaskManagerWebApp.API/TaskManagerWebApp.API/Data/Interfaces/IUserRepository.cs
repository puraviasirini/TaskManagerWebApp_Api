using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Users AuthenticateUser(LoginCredentials loginCredentials);
        string GenerateToken(Users users);
    }
}
