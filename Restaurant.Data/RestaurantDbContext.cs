using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data.Entities;

namespace Restaurant.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MenuCardEntity> MenuCard { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
    }
}
