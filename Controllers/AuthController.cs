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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;

    public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {

        //await _emailSender.SendEmailAsync("bacembalti3@gmail.com", "Test Subject", "This is a test email.");

        // test get list Cart done
        // à virer le mettre dans les services
        // Linq query
        var test = _dbContext.Carts.Select(cart => cart.UserId).ToList();


        // Jointure Order => OrderItems
        // Query synatxe
        var query = (from order in _dbContext.Orders
                     join orderItem in _dbContext.OrderItems on order.Id equals orderItem.OrderId
                     select new
                     {
                         Id_orderTable = order.Id,
                         Id_orderItemsTable = orderItem.Id,
                         UnitPrice = orderItem.UnitPrice,
                     })
                     .ToList();

        // _userManager et _roleManager deux Classes fourni par le package pour récupérer les users ou roles de la base
        var users = _userManager.Users.ToList();
        var roless = _roleManager.Roles.ToList();


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
