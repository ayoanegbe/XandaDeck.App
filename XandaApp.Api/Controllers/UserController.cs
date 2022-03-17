using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XandaApp.App.Services;
using XandaApp.Data.Context;
using XandaApp.Data.Models;
using XandaApp.Infra.Services;

namespace XandaApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;

        //[Inject]
        public UserController(UserService userService)
        {
            _userService = userService;
            _context = new ApplicationDbContext();
        }

        [HttpGet("api-check")]
        public IActionResult Check()
        {
            return Ok(new {message = "I'm okay"});
        }
        /// <summary>
        /// Registers user.
        /// </summary>
        /// <returns>Registration status.</returns>
        // Post: api/user/RegisterAsync
        [HttpPost("register")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> RegisterAsync(User model)
        {
            if (!IsDatabaseSeeded())
            {
                return BadRequest(new { message = "Database not seeded" });
            }

            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Get token.
        /// </summary>
        /// <returns>Authentication model.</returns>
        // Post: api/user/GetTokenAsync
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequest model)
        {
            if (!IsDatabaseSeeded())
            {
                return BadRequest(new { message = "Database not seeded" });
            }

            var result = await _userService.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            if (!IsDatabaseSeeded())
            {
                return BadRequest(new { message = "Database not seeded" });
            }

            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!IsDatabaseSeeded())
            {
                return BadRequest(new { message = "Database not seeded" });
            }

            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(string Token)
        {
            if (!IsDatabaseSeeded())
            {
                return BadRequest(new { message = "Database not seeded" });
            }

            // accept token from request body or cookie
            var token = Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _userService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [Authorize]
        [HttpPost("tokens/{id}")]
        public async Task<IActionResult> GetRefreshTokens(string id)
        {
            var user = await _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        private protected bool IsDatabaseSeeded()
        {
            _context.Database.EnsureCreated();

            // Look for any users.
            if (_context.Users.Any())
            {
                return true;
            }

            return false;
        }
    }
}
