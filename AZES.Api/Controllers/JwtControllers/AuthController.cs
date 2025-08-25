using Azure.Core;
using BRIT.Models.DtoEntities;
using BRIT.Models.Entities;
using BRIT.Models.JwtDtoEtities;
using BRIT.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AZES.Api.Controllers.JwtControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult<object> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);

            //var userName = User?.Identity?.Name;
            //var userName2 = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);
            //return Ok(new { userName, userName2, role });
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Kennwort, out byte[] passwordHash, out byte[] passwordSalt);

            user.Vorname = request.Vorname;
            user.Nachname = request.Nachname;
            user.KennwortHash = passwordHash;
            user.KennwortSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            string usernameFromLoginReqest = request.Nachname + request.Vorname;
            string usernameFromRegistrationUser = user.Nachname + user.Vorname;

            if (usernameFromRegistrationUser != usernameFromLoginReqest)
            {
                return BadRequest("User ist nicht gefunden.");
            }

            if (!VerifyPasswordHash(request.Kennwort, user.KennwortHash, user.KennwortSalt))
            {
                return BadRequest("Kennwort ist falsch.");
            }
            string token = CreateToken(user);
            return Ok(token);
        }
        
        private string CreateToken(User user)
        {
            string usernameFromRegistrationUser = user.Nachname + user.Vorname;
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usernameFromRegistrationUser),

                //new Claim(ClaimTypes.NameIdentifier, usernameFromRegistrationUser), // bei der Verwendung in Datenbanken ist <ClaimTypes.NameIdentifier> vorgesehen
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
