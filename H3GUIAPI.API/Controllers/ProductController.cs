using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H3GUIAPI.API.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	readonly ProductContext _productContext;

	public ProductController(ProductContext productContext)
	{
		_productContext = productContext;
	}

	[HttpGet]
	public async Task<IEnumerable<Product>> GetAll()
	{
		try
		{
			var products = _productContext.Products.Include(p => p.Category);
			return await products.ToArrayAsync();
		}
		catch
		{
			return [];
		}
	}

	[HttpGet("{id}")]
	public ActionResult<Product> GetSingle(uint id)
	{
		try
		{
			return Ok(_productContext.Products.Single(p => p.ProductId == id));
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
			return await _productContext.Products.Where(p => p.CategoryId == id).Include(p => p.Category).ToArrayAsync();
		}
		catch
		{
			return [];
		}
	}

	public record PostBody(Product Product, ImageFile ImageFile);


	[HttpPost]
	public async Task<ActionResult> Create([FromBody] PostBody body)
	{
		ImageFilePageData imageFilePageData = new(body.ImageFile.FileName) { Product = body.Product };

		try
		{
			await _productContext.ImageFilesDatas.AddAsync(imageFilePageData);
			await _productContext.Products.AddAsync(body.Product);
			if (!await ImageFile.Save(body.ImageFile))
			{
				throw new Exception();
			}
			await _productContext.SaveChangesAsync();
			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest("file already exists");
		}

	}

}
