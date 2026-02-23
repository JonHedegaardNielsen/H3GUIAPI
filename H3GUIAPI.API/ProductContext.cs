using H3GUIAPI.API.Models;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;

namespace H3GUIAPI.API;

public class ProductContext : DbContext
{
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<ImageFilePageData> ImageFilesDatas => Set<ImageFilePageData>();

	public string DbPath { get; }

	public ProductContext(DbContextOptions<ProductContext> options) : base(options)
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		var path = Environment.GetFolderPath(folder);
		DbPath = Path.Join(path, "products.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Product>()
			.HasOne<ImageFilePageData>(e => e.ImageFilePageData);

		modelBuilder.Entity<ImageFilePageData>()
			.HasKey(e => e.ImageFilePathDataId);

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={DbPath}");
		base.OnConfiguring(optionsBuilder);
	}

}
