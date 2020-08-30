using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using web_identity_csharp_base.Models;
using System.Linq;
using web_identity_csharp_base.Services;
using System.Security.Claims;
using System.Collections.Generic;

namespace web_identity_csharp_base.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private readonly IEmailSender _emailSender;

        // public AuthenticationController(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        // {
        //     _emailSender = emailSender;
        //     _userManager = userManager;
        // }

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                if ((await _roleManager.RoleExistsAsync("Member")) == false){
                    var role = new IdentityRole("Member");
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (roleResult.Succeeded == false){
                        var errorsRole = string.Join(',', roleResult.Errors.Select(sx => sx.Description));
                        ModelState.AddModelError("SignUp", errorsRole);
                        return BadRequest(ModelState);
                    }
                }


                if ((await _userManager.FindByEmailAsync(model.Email)) == null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Member");
                        return Ok();
                        //return RedirectToAction("SignIn");
                    }

                    ModelState.AddModelError("SignUp", string.Join("", result.Errors.Select(x => x.Description)));
                    return BadRequest(ModelState);
                }
            }
            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Username);
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    // if (await _userManager.IsInRoleAsync(user, "Member"))
                    //     RedirectToAction("Member");

                    // if (!userClaims.Any(x => x.Type == "Department" && x.Value == "Sales"))
                    //     RedirectToAction("Vendas");

                    IDictionary<string, string> data = new Dictionary<string, string>();
                    foreach (var claimUser in userClaims)
                    {
                        data.Add(claimUser.Type, claimUser.Value);
                    }
                    
                    
                    return Ok(data);

                }
                else
                {
                    ModelState.AddModelError("SignIn", "Falha no Login");
                    return BadRequest(ModelState);
                }
            }
            else{
                return BadRequest();
            }
        }


        [HttpGet("SignOut")]        
        public async Task<IActionResult> SignOut(){
            await _signInManager.SignOutAsync();
            return Forbid();
        }


    }
}