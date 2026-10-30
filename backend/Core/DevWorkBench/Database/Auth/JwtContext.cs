using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DevWorkBench
{
    public class JwtContext : DbContext
    {

        public DbSet<Jwt> Jwts {get;set;}
        public JwtContext (DbContextOptions<JwtContext> options) : base(options)
        {}

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }  

    }

}