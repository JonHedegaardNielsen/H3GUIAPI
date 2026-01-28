using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3GUIAPI.API.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<Product>> GetAll()
	{
		try
		{
			using ProductContext context = new();
			var products = context.Products.Include(p => p.Category);
			return await products.ToArrayAsync();
		}
		catch
		{
			return [];
		}
	}

	[HttpPost]
	public async Task<ActionResult> Add([FromBody] Product product)
	{
		try
		{
			using ProductContext context = new();
			context.Products.Add(product);
			await context.SaveChangesAsync();
			return Ok();
		}
		catch
		{
			return NotFound();
		}
	}

	[HttpGet("{id}")]
	public ActionResult<Product> GetSingle(uint id)
	{
		try
		{
			using ProductContext context = new();
			return Ok(context.Products.Single(p => p.ProductId == id));
		}
		catch
		{
			return NotFound();
		}
	}

	[HttpGet("FromCategoryId/{id}")]
	public async Task<IEnumerable<Product>> GetFromCategoryId(uint id)
	{
		try
		{
			using ProductContext context = new();
			return await context.Products.Where(p => p.CategoryId == id).Include(p => p.Category).ToArrayAsync();
		}
		catch
		{
			return [];
		}
	}
}


