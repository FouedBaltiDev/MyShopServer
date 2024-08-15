using Microsoft.AspNetCore.Identity;
using MyShop.Models;

namespace MyShop.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string userId);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(string userId);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IdentityResult> AssignRoleToUserAsync(User user, string roleName);
    bool IsValidEmail(string email);
}

