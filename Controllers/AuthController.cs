using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreWebAPI.Data;
using AspNetCoreWebAPI.DTOs;
using AspNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWebAPI.Controllers
{
     
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO UserRegisterDTO)
        {
            UserRegisterDTO.UserName = UserRegisterDTO.UserName.ToLower();
            if (await _repo.UserExists(UserRegisterDTO.UserName))
            {
                return BadRequest("User already Exist");
            }
            var usercreate = new Users
            {
                Username = UserRegisterDTO.UserName
            };
            var createduser = await _repo.Register(usercreate, UserRegisterDTO.Password);
            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO LoginDTO)
        {

            LoginDTO.UserName = LoginDTO.UserName.ToLower();
            var logincred = await _repo.Login(LoginDTO.UserName.ToLower(), LoginDTO.Password);
            if (logincred == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logincred.UserID.ToString()),
                new Claim(ClaimTypes.Name,logincred.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescriptor);
            return Ok(new
            {
                token = tokenhandler.WriteToken(token)
            });
        }

        [HttpGet]
        [Route("/Error/{id?}")]
        public string Error(int? id)
        {
            if (id == 404)
            {
                return "PageNotFound";
            }
            return "ko";

        }
        [AllowAnonymous]
        [HttpGet]
        public  String Get()
        {
            return ("HI");
        }
    }
}