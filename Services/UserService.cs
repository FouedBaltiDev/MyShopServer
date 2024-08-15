using Microsoft.AspNetCore.Identity;
using MyShop.Models;
using System.Net.Mail;

namespace MyShop.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _userManager.Users; // Note: Consider using AsEnumerable() if required
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
}

