using Microsoft.AspNetCore.Mvc;

namespace netcore.Modules.Users
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto dto)
        {
            var user = await _service.CreateAsync(dto);
            return Ok(user);
        }
    }
}