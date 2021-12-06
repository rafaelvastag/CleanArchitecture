using Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticateService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false
                , lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var register = await _userManager.CreateAsync(user, password);

            if (register.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return register.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
