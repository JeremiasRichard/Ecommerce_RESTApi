using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RemitoApi.DTOs.Secutiry;
using RemitoApi.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RemitoApi.Services
{
    public class UserServiceImp
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly IDataProtector _dataProtector;

        public UserServiceImp(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)//, IDataProtectionProvider dataProtectionProvider)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            //_dataProtector = dataProtectionProvider.CreateProtector("testingProtectorProvider");
        }

        public async Task<AuthenticationResponse> RegisterUser(UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email, };
            var userExist = await _userManager.FindByEmailAsync(userCredentials.Email);
            if (userExist == null)
            {
                var result = await _userManager.CreateAsync(user, userCredentials.Password);
                if (result.Succeeded)
                {

                    return await BuildToken(userCredentials);
                }
                else
                {
                    return null;
                }
            }

            throw new CustomException("Email already exist!", 400);
        }

        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,
               userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return null;
            }

        }

        public async Task<AuthenticationResponse> RenewToken(UserCredentials userCredentials)
        {
            return await BuildToken(userCredentials);
        }

        private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",userCredentials.Email),
            };

            var user = await _userManager.FindByEmailAsync(userCredentials.Email);

            var claimsDB = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyJwt"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims
                , expires: expiration, signingCredentials: credentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
                Id = user.Id
            };
        }

        public async Task<IdentityResult> GetAdminPermissions(EditAdminDTO editAdminDTO)
        {
            var user = await _userManager.FindByEmailAsync(editAdminDTO.Email);
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, "admin");
                return result;
            }

            return null;
        }
    }
}
