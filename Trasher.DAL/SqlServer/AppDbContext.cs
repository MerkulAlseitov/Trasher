using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Users;

namespace Trasher.DAL.SqlServer
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Brigade)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BrigadeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
           .HasOne(o => o.Operator)
           .WithMany(op => op.Orders)
           .HasForeignKey(o => o.OperatorId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
           .HasOne(o => o.Review)
           .WithOne(r => r.Order)
           .HasForeignKey<Order>(o => o.ReviewId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
