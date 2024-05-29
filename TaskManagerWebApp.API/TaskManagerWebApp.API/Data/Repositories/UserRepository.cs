using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerWebApp.API.Data.Interfaces;
using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly TaskManagerDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        public UserRepository(TaskManagerDbContext context , IConfiguration configuration) 
        {
            _dbcontext = context;
            _configuration = configuration;
        }
        public Users AuthenticateUser(LoginCredentials loginCredentials)
        {
            //Users _users = null;
            //if (users.UserName == "puravi" && users.Password == "1234")
            //{
            //    _users = new Users { UserName = "puravi" };
            //    // _users = new Users { UserName = "puravi", Role = "Admin" };
            //}

            //return _users;
            var user = _dbcontext.Users.FirstOrDefault(u => u.UserName == loginCredentials.UserName && u.Password == loginCredentials.Password);
            return user;
        }

        public string GenerateToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.UserName),
                new Claim(ClaimTypes.Role, users.Role), // Assuming you store the user's role in the database
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
