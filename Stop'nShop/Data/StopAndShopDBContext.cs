using Microsoft.EntityFrameworkCore;

namespace Stop_nShop.Data
{
    public class StopAndShopDBContext : DbContext
    {
        public StopAndShopDBContext(DbContextOptions<StopAndShopDBContext> options)
       : base(options)
        {
        }

        public DbSet<Stop_nShop.Models.User> Users { get; set; } = default!;

        public DbSet<Stop_nShop.Models.Seller> Sellers { get; set; } = default!;

        public DbSet<Stop_nShop.Models.Product> Products { get; set; } = default!;

        public DbSet<Stop_nShop.Models.Orders> Orders { get; set; } = default!;

        public DbSet<Stop_nShop.Models.Interested> Interested { get; set; } = default!;

    }
}
