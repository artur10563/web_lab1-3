using Labs_WEB.Models;

namespace Labs_WEB.Extensions
{

	public static class SeedingExtensions
	{
		public static void Seed(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var context = scope.ServiceProvider.GetService<AppDbContext>();

				context.Database.EnsureCreated();

				try
				{

					if (context.Items.Count() == 0)
					{

						context.Items.AddRange(
						[
							new Item { Name = "Iphone15", Price = 1000, Quantity = 10 },
							new Item { Name = "Microwave", Price = 200, Quantity = 50 },
							new Item { Name = "Computer", Price = 2000, Quantity = 5 }
						]
						);

						context.SaveChanges();
					}

					if (context.Tags.Count() == 0)
					{
						context.Tags.AddRange(
						[
							new Tag {Name = "Телефон"  },
							new Tag {Name = "Техніка для дому"  },
							new Tag {Name = "Деталі"  }
						]
						);
						context.SaveChanges();

					}
				}
				catch (Exception ex)
				{

					throw new Exception(ex.Message);
				}

			}
		}
	}
}
