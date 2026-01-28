using H3GUIAPI.API.Models;
using Microsoft.EntityFrameworkCore;

namespace H3GUIAPI.API;

public class ProductContext : DbContext
{
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Category> Categories => Set<Category>();
	public string DbPath { get; }

	public ProductContext()
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		var path = Environment.GetFolderPath(folder);
		DbPath = System.IO.Path.Join(path, "blogging.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={DbPath}");
		base.OnConfiguring(optionsBuilder);
	}

}
