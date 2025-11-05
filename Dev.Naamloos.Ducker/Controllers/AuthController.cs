using Dev.Naamloos.Ducker.Database;
using Dev.Naamloos.Ducker.Database.Entities;
using Dev.Naamloos.Ducker.Dto;
using Dev.Naamloos.Ducker.Services;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dev.Naamloos.Ducker.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _users;
        private readonly SignInManager<User> _signIn;
        private readonly IConfiguration _cfg;
        private readonly AppDbContext _db;

        public AuthController(UserManager<User> users, SignInManager<User> signIn, IConfiguration cfg, AppDbContext db)
        {
            _users = users;
            _signIn = signIn;
            _cfg = cfg;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            User newUser = new()
            {
                UserName = dto.Username,
                Email = dto.Email,
                EmailConfirmed = true // we dont do email confirmation.
            };

            var result = await _users.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Add any claims, for now none.

            await _signIn.SignInAsync(newUser, isPersistent: true);

            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }
            var result = await _signIn.PasswordSignInAsync(user, dto.Password, isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok("Logged in");
        }

        [HttpGet("login")]
        public Task<IActionResult> LoginPage()
        {
            return Task.FromResult<IActionResult>(Inertia.Render("login"));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return Ok("Logged out");
        }
    }
}
