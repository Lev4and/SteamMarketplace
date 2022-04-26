using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SteamMarketplace.Authorization.Common;
using SteamMarketplace.Model.Database.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SteamMarketplace.AuthorizationWebApplication.Services
{
    public class Authorization
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Authorization(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private async Task<string> GetRoleAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).First();
        }

        public async Task<string> GenerateJWTAsync(ApplicationUser user)
        {
            var securityKey = AuthorizationOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("role", await GetRoleAsync(user)),
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.Id}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var token = new JwtSecurityToken(issuer: AuthorizationOptions.Issuer, audience: AuthorizationOptions.Audience,
                claims: claims, expires: DateTime.Now.AddSeconds(AuthorizationOptions.TokenLifetime), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplicationUser> AuthenticateUserAsync(string emailOrLogin, string password)
        {
            var userName = await _userManager.FindByNameAsync(password);
            var userEmail = await _userManager.FindByEmailAsync(emailOrLogin);

            if (userEmail != null || userName != null)
            {
                var user = userEmail != null ? userEmail : userName;

                if ((await _signInManager.PasswordSignInAsync(user, password, false, false)).Succeeded)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
