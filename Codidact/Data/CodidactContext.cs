using System;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Codidact
{
    public partial class CodidactContext : IdentityDbContext<Users, Role, int>
    {        public CodidactContext()
        {
        }

        public CodidactContext(DbContextOptions<CodidactContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CodiDactContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
