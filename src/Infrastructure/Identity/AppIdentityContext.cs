using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityContext : IdentityDbContext<AppUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
