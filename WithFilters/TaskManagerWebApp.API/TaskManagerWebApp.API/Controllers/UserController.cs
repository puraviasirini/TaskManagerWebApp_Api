using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApp.API.Data.Interfaces;
using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Login(Users users)
        {
            IActionResult response = Unauthorized();
            var user = _userRepository.AuthenticateUser(users);
            if (user != null)
            {
                var token = _userRepository.GenerateToken(user);
                response = Ok(new { token = token });
            }

            return response;
        }
    }
}
