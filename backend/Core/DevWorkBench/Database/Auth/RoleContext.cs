using Microsoft.EntityFrameworkCore;

using DevWorkBench;
    public class RoleContext : DbContext
    {
        public DbSet<Role> Roles {get;set;}

        public RoleContext (DbContextOptions<RoleContext> options) : base(options) // Role Context Options 
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


    }
