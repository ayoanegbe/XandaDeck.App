using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Models;

namespace XandaApp.Infra.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetCurrentUser();
        Task<string> RegisterAsync(User model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequest tokenRequest);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string jwtToken);
        bool RevokeToken(string token);
        Task<ApplicationUser> GetById(string id);
    }
}
