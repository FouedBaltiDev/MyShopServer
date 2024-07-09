using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyShop.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;

    public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _configuration = configuration;
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // test get list Cart done
        // à virer le mettre dans les services
        // Linq query
        var test = _dbContext.Carts.Select(cart => cart.UserId).ToList();

        // var rrr = _dbContext.AspNetuser

        var user = await _userManager.FindByNameAsync(model.UserName);

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, model.Password);

        if (user != null && model.Password == user.PasswordHash)
        {
            // Récupérer le rôle de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); // Supposons que l'utilisateur a un seul rôle

            // Créer les revendications pour le token JWT
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Role, role ?? string.Empty)  // Ajouter le rôle ici
            };

            // Clé de signature pour JWT
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Création du token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // Retourner le token JWT et sa date d'expiration
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        // Si l'utilisateur n'est pas authentifié
        return Unauthorized();
    }

    public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
