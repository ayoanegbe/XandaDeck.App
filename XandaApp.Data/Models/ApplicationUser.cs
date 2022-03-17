using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace XandaApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        : base()
        {
        }

        public ApplicationUser(string userName)
        : base(userName)
        {
            base.Email = userName;
            base.UserName = userName;
        }

        public int? AccounId { get; set; }
        public bool DeviceLogin { get; set; } = false;

        public DateTimeOffset? LastLogin { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
