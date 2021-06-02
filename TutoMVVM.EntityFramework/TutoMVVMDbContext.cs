using Microsoft.EntityFrameworkCore;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.EntityFramework
{
    public class TutoMVVMDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        public TutoMVVMDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);

            base.OnModelCreating(modelBuilder);
        }
    }
}
