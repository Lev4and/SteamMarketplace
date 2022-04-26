using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Authorization.Models
{
    public class Login
    {
        [Required]
        public string EmailOrLogin { get; set; }

        [Required]
        public string Password { get; set; }

        public Login(string emailOrLogin, string password)
        {
            if (string.IsNullOrEmpty(emailOrLogin))
            {
                throw new ArgumentNullException("emailOrLogin", "The email or login must not be empty.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "The password must not be empty.");
            }

            EmailOrLogin = emailOrLogin;
            Password = password;
        }
    }
}
