namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyShop.Dtos; // Ajoutez cette ligne pour importer le namespace des DTO
    using MyShop.Models;
    using MyShop.Services;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); // Return all users as JSON
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Return 404 if user is not found
            }
            return Ok(user); // Return the user as JSON
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if model state is invalid
            }

            if (registrationDto.Password == null)
            {
                return BadRequest(new { message = "Password is required" }); // Return 400 if password is null
            }

            try
            {
                var user = new User
                {
                    UserName = registrationDto.UserName,
                    Email = registrationDto.Email
                };

                await _userService.CreateUserAsync(user, registrationDto.Password);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user); // Return 201 Created
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Return 400 with error message
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest(); // Return 400 if ID mismatch
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if model state is invalid
            }

            try
            {
                await _userService.UpdateUserAsync(user);
                return NoContent(); // Return 204 No Content if update is successful
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Return 400 with error message
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent(); // Return 204 No Content if deletion is successful
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Return 400 with error message
            }
        }
    }
}
