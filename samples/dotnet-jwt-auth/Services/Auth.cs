using Dotnet_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Dotnet_Backend 
{
    public class AuthService
    {
        private IConfiguration _configuration;
        const int accessTokenExpiryDays = 1;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(ApplicationUser user)
        {
            List<Claim> AuthClaims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),

                new Claim (JwtRegisteredClaimNames.Email,
                    user.Email),

                new Claim (JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),
			
			    // Add the ClaimType Role which carries the Role of the user
			    new Claim (ClaimTypes.Role.ToString(), user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: AuthClaims,
                expires: DateTime.Now.AddDays(accessTokenExpiryDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsInRole(ApplicationUser user, UserRole role)
        {
            return user.Role.ToString().Equals(role.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public AuthResponse RegisterUser(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
                return new AuthResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                PasswordHash = model.Password
            };

            Users.Add(user);

            return new AuthResponse
            {
                //Message = result.Succeeded ? "User created successfully!" : "User did not create",
                IsSuccess = true,
                //Errors = result.Errors.Select(e => e.Description)
            };
        }

        public AuthResponse LoginUser(LoginViewModel model)
        {
            var user = FindByEmail(model.Email);

            if (user == null)
            {
                return new AuthResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = CheckPassword(user, model.Password);

            if (!result)
                return new AuthResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            // Generate AccessToken
            string tokenAsString = GenerateAccessToken(user);

            return new AuthResponse
            {
                AccessToken = tokenAsString,
                IsSuccess = true,
                ExpireDate = DateTime.Now.AddDays(accessTokenExpiryDays)
            };
        }

        private ApplicationUser FindByEmail(string email)
            => Users.FirstOrDefault(model => model.Email == email);
        
        private bool CheckPassword(IdentityUser user, string password)
        {
            return user.PasswordHash == password;
        }

        private readonly List<ApplicationUser> Users = new List<ApplicationUser> { 
            // TODO: Encrypt
            new ApplicationUser("Admin") { Email = "admin@example.com", PasswordHash = "1234567", Role = UserRole.Admin },
            new ApplicationUser("User") { Email = "user@example.com", PasswordHash = "1234567", Role = UserRole.User }
        };
    }
}