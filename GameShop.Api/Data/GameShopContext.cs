using GameShop.Api.Models;
using GameShop.Api.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Data
{
    public class GameShopContext : DbContext
    {
        public GameShopContext(DbContextOptions<GameShopContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameOrder> GameOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).LastEditTime = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateTime = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .Property(g => g.Platform)
                .HasConversion<string>();

            modelBuilder.Entity<Game>()
                .Property(g => g.Publisher)
                .HasConversion<string>();
        }
    }
}
