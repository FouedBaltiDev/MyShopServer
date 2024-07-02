using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class UserRole : IdentityUser
{
    [Required]
    public string? Role { get; set; }

}