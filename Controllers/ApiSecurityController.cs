using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using web_identity_csharp_base.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

namespace web_identity_csharp_base.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ApiSecurityController: Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public ApiSecurityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration){
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;

        }

        [HttpGet("tokenauth")]
        [AllowAnonymous]
        public async Task<IActionResult> TokenAuth(SignInModel signInModel){
            

            if (ModelState.IsValid){
                var signInResult = await _signInManager.PasswordSignInAsync(signInModel.Username,
                    signInModel.Password, signInModel.RememberMe, false);
                if (signInResult.Succeeded){
                    var user = await _userManager.FindByEmailAsync(signInModel.Username);
                    if (user != null){
                        var token = GenerateToken(user);
                        return Ok(token);
                    }
                }
            }

            return BadRequest();

        }

        private string GenerateToken(IdentityUser user)
        {
            var issuer = _configuration["Tokens:Issuer"];
            var audience = _configuration["Tokens:Audience"];
            var secret = _configuration["Tokens:Key"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(type:JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(type:JwtRegisteredClaimNames.Jti, user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}