namespace MyShop.Controllers
{
    using Microsoft.AspNetCore.Identity;
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

            if (string.IsNullOrEmpty(registrationDto.Password))
            {
                return BadRequest(new { message = "Password is required" }); // Return 400 if password is null
            }

            // en c# les string qui commence avec dollar $ on appelle ce syntax => string interpolation (mix string et variables)
            if (!_userService.IsValidEmail(registrationDto.Email))
            {
                return BadRequest(new { message = $"Email address '{registrationDto.Email}' is invalid" });
            }

            try
            {
                var user = new User
                {
                    UserName = registrationDto.UserName,
                    Email = registrationDto.Email
                };


                var result = await _userService.CreateUserAsync(user, registrationDto.Password);

                if (result.Succeeded)
                {
                    // Assigner le rôle par défaut à l'utilisateur
                    var roleResult = await _userService.AssignRoleToUserAsync(user, "User");

                    if (!roleResult.Succeeded)
                    {
                        // Gérez l'échec de l'assignation du rôle ici
                        return BadRequest(roleResult.Errors);
                    }

                    // Retourner 201 Created
                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
                }

                // Si la création échoue, retourner les erreurs
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                //var message = result;
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



        [HttpPost("updateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UserRoleUpdateDto updateRoleDto)
        {
            if (string.IsNullOrEmpty(updateRoleDto.UserId) || string.IsNullOrEmpty(updateRoleDto.NewRole))
            {
                return BadRequest("User ID and new role must be provided.");
            }

            // Appel de la méthode du service pour mettre à jour le rôle
            IdentityResult result = await _userService.UpdateUserRole(updateRoleDto.UserId, updateRoleDto.NewRole);

            if (result.Succeeded)
            {
                return Ok(new { message = "Role updated successfully." });
            }
            else
            {
                // Récupérer les messages d'erreur pour les retourner
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errors);
            }
        }
    }
}
