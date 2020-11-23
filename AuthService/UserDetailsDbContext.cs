using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public class UserDetailsDbContext : DbContext
    {
        public UserDetailsDbContext(DbContextOptions<UserDetailsDbContext> options) : base(options)
        {
        }
        public virtual DbSet<UserDetails> User { get; set; }
    }
}
