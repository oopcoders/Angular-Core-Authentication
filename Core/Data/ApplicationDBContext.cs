using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		public DbSet<Value> Values { get; set; }
	}
}