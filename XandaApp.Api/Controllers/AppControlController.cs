using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using XandaApp.Data.Context;
using XandaApp.Data.Contexts;
using XandaApp.Data.Models;
using XandaApp.App.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using XandaApp.Infra.Services;

namespace XandaApp.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControlController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        private readonly IConfiguration config;


        public AppControlController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger logger)
        {
            _context = new ApplicationDbContext();

            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;

            config = AppServices.GetConfiguration();
        }

        // Post: api/settings/SeedDatabase
        [HttpPost("seed-db")]
        public ActionResult SeedDatabase()
        {
            _context.Database.EnsureCreated();

            // Look for any users.
            if (_context.Users.Any())
            {
                return BadRequest(new { message = "Database already seeded" });
            }

            return Ok();
        }

        [HttpGet("startup")]
        public IActionResult Startup()
        {

            bool successful = false;
            try
            {

                string appName = Path.GetFileNameWithoutExtension(config["AppSettings:AppName"]);
                int count = Process.GetProcessesByName(appName).Count();
                var exists = count >= 1;
                if (exists)
                {
                    return Ok("An instance is already running");
                }

                string fileName = config["AppSettings:AppName"];
                //string filePath = config["AppSettings:AppPath"]; // This will be used eventually
                string path = Path.Combine(AppContext.BaseDirectory, fileName);

                Process process = new Process();

                process.StartInfo.FileName = path;

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                successful = process.ExitCode == 0;
                process.Dispose();

                return Ok(successful);
            }
            catch (Exception /*ex*/)
            {
                return Ok(successful);
            }
        }

        private protected async Task<bool> SeedAsync()
        {
            try
            {
                //Seed Default Users
                await ApplicationDbContextSeed.SeedEssentialsAsync(_userManager, _roleManager);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred seeding the DB.");
                return false;
            }
            return true;
        }
    }
}
