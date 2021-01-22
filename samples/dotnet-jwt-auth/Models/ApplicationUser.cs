using Microsoft.AspNetCore.Identity;
using System;

namespace Dotnet_Backend.Models
{
    public enum UserRole 
    {
        User,
        Admin
    }

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public ApplicationUser(string userName) : base(userName)
        {
            UserName = userName;
        }

        public UserRole Role { get; set; }
    }
}
