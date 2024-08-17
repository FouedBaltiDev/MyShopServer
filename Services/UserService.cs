using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Dtos;
using MyShop.Models;
using System.Net.Mail;

namespace MyShop.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;


    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        // Jointure entre (AspNetUsers, AspNetRoles, AspNetUserRoles)
        // Ici on a fait une jointure car le role existe dans une autre table qui est AspNetRoles et pas dans la table AspNetUsers
        var usersWithRoles = await (from user in _dbContext.Users
                                    join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                                    join role in _dbContext.Roles on userRole.RoleId equals role.Id

                                    // Le keyword select new on l'appelle projection comme le select sql
                                    select new UserDto
                                    {
                                        Id = user.Id,
                                        Role = role.Name,
                                        UserName = user.UserName,
                                        Email = user.Email
                                    }).ToListAsync();

        return usersWithRoles;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        // Crée un nouvel utilisateur avec un mot de passe
        var result = await _userManager.CreateAsync(user, password);

        return result; // Retourner le résultat de la création de l'utilisateur
    }

    public async Task UpdateUserAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            // Validation de la syntaxe
            var mailAddress = new MailAddress(email);
            if (mailAddress.Address != email)
            {
                return false;
            }
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(User user, string roleName)
    {
        // Remarque : Ici on travaille sur la table AspNetRoles en base parceque (IdentityRole = AspNetRoles)
        // Les champs dans IdentityRole sont les mêmes dans AspNetRoles
        // Donc ici (_roleManager.CreateAsync(new IdentityRole(roleName))) ce code ajoute une ligne dans AspNetRoles


        // équivalent de la table AspNetUsers est la classe c# => IdentityUserRole

        // Vérifiez si le rôle existe
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            // Créez le rôle s'il n'existe pas
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                return roleResult; // Retourner les erreurs si la création du rôle échoue
            }
        }

        // Assigner le rôle à l'utilisateur
        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result; // Retourner le résultat de l'assignation
    }

    // Maybe we create a service for role separated to user service
    public async Task<IdentityResult> UpdateUserRole(string userId, string newRole)
    {
        // Récupérer l'utilisateur par ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "User not found.", // Vous pouvez définir un code d'erreur personnalisé si nécessaire
            });
        }

        // Vérifier si le rôle existe
        if (!await _roleManager.RoleExistsAsync(newRole))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Role does not exist.", // Vous pouvez définir un code d'erreur personnalisé si nécessaire
            });
        }

        // Récupérer les rôles actuels de l'utilisateur
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Si l'utilisateur a déjà le rôle désiré, rien ne change
        if (currentRoles.Contains(newRole))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "User already has the specified role", // Vous pouvez définir un code d'erreur personnalisé si nécessaire
            });
        }

        // Supprimer tous les rôles actuels
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Error removing current roles.", // Vous pouvez définir un code d'erreur personnalisé si nécessaire
            });
        }

        // Ajouter le nouveau rôle
        var addResult = await _userManager.AddToRoleAsync(user, newRole);
        if (!addResult.Succeeded)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Error adding new role.", // Vous pouvez définir un code d'erreur personnalisé si nécessaire
            });
        }

        return IdentityResult.Success;
    }
}

