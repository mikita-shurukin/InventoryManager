using InventoryManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InventoryManager.DAL
{
    public class MainDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await Orders.FindAsync(orderId);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryItem>()
                .HasKey(x => new { x.WarehouseId, x.ItemId });
        }
    }
}
