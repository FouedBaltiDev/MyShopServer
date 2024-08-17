using Microsoft.AspNetCore.Identity;
using MyShop.Dtos;
using MyShop.Models;

namespace MyShop.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string userId);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(string userId);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IdentityResult> AssignRoleToUserAsync(User user, string roleName);
    Task<IdentityResult> UpdateUserRole(string userId, string newRole);
    bool IsValidEmail(string email);
}

