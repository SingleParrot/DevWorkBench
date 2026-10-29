using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace DevWorkBench
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users {get;set;}

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }   
}