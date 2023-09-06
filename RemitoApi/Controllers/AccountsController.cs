using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using RemitoApi.Services;
using RemitoApi.DTOs.Secutiry;

namespace RemitoApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserServiceImp _userService;
        public AccountsController(UserServiceImp userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials)
        {
            var result = await _userService.RegisterUser(userCredentials);
            if (result != null)
            {
                return result;
            }

            return BadRequest("Error registering user");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await _userService.Login(userCredentials);

            if (result != null)
            {
                return result;
            }

            return BadRequest("Invalid login!");
        }

        [HttpGet("RenewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthenticationResponse>> Renew()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            if (email == null)
            {
                return BadRequest("Token null!");
            }

            var userCredentials = new UserCredentials()
            {
                Email = email,
            };

            return await _userService.RenewToken(userCredentials);
        }

        /*[HttpPost("MakeAdmin")]
        public async Task<ActionResult> MakeAdmin(EditAdminDTO editAdminDTO)
        {
            return Ok(await _userService.GetAdminPermissions(editAdminDTO));
        }

        [HttpPost("RemoveAdmin")]
        public async Task<ActionResult> RemoveAdmin(EditAdminDTO editAdminDTO)
        {
            var user = await _userManager.FindByEmailAsync(editAdminDTO.Email);
            await _userManager.RemoveClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }*/
    }
}
