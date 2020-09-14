using Core.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
	public class ApplicationDBContext : IdentityDbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		public DbSet<Value> Values { get; set; }
	}
}