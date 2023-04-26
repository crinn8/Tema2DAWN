using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tema2.DTOs;
using Tema2.Models;
using Tema2.Services;
using System.Security.Claims;

namespace Tema2.Controllers
{
    [ApiController]
    [Route("account")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto payload)
        {
            bool isUnique = await _userService.CheckUsername(payload.Username);

            if (!isUnique)
            {
                return BadRequest("Username is already used!");
            }

            bool result = await _userService.RegisterUser(payload);

            if (!result)
            {
                return BadRequest("Register failed!");
            }

            return Ok();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto payload)
        {
            var jwtToken = await _userService.ValidateUser(payload);

            if (jwtToken == "" || jwtToken == null)
            {
                return BadRequest("Wrong credentials.");
            }
            else
            {
                return Ok(new { token = jwtToken });
            }
        }

        [HttpGet("test-auth")]
        public IActionResult TestLogin()
        {
            ClaimsPrincipal user = User;
            var result = "";

            foreach (var claim in user.Claims)
            {
                result += claim.Type + " : " + claim.Value + "\n";
            }

            var hasRole_user = user.IsInRole("User");
            var hasRole_teacher = user.IsInRole("Teacher");

            return Ok(result);
        }

        [HttpGet("only-student")]
        [Authorize(Roles = "Student")]
        public ActionResult<string> HelloStudents()
        {
            return Ok("Hello students!");
        }

        [HttpGet("only-teacher")]
        [Authorize(Roles = "Teacher")]
        public ActionResult<string> HelloTeachers()
        {
            return Ok("Hello teachers!");
        }

        [HttpGet("get-all-by-role")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAllByRole()
        {
            var result = await _userService.GetAllUsersByRole();

            if(result == null)
            {
                return BadRequest("Error in getting users!");
            }
            return Ok(result);
        }
    }
}
