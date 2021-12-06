using Domain.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticateService _authentication;
        private readonly IConfiguration _configuration;

        public LoginController(IAuthenticateService authentication, IConfiguration configuration)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserLoginDTO userInfos)
        {
            var result = await _authentication.Authenticate(userInfos.Email, userInfos.Password);

            if (result)
            {
                return GenerateToken(userInfos);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserTokenDTO GenerateToken(UserLoginDTO userInfos)
        {
            var claims = new[]
            {
                new Claim("email", userInfos.Email),
                new Claim("owner", "Vastag'sWebApi"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // create secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:SecretKey"]));

            // create digital sign
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Expiration time
            var expiration = DateTime.UtcNow.AddHours(1);

            // Generation Token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Security:Issuer"],
                audience: _configuration["Security:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
