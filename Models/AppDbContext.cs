using Microsoft.EntityFrameworkCore;

namespace Labs_WEB.Models
{
	public class AppDbContext : DbContext
	{
		public DbSet<Item> Items { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
		   : base(options)
		{
		}

	}
}
