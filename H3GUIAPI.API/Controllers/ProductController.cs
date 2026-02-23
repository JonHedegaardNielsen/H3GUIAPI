using H3GUIAPI.API.DTOs;
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
	public async Task<IEnumerable<ProductGetBody>> GetAll()
	{
		try
		{
			var products = await _productContext.Products.Include(p => p.Category)
				.Select((p) =>
					new
					{
						p.ProductId,
						p.Title,
						p.Category,
						p.Price,
						Path = p.ImageFilePageData.RelativePath
					}
				).ToArrayAsync();
			List<ProductGetBody> productGetBodies = new(products.Length);
			foreach (var product in products)
			{
				productGetBodies.Add(new ProductGetBody(product.ProductId, product.Title, product.Category, product.Price, await ImageFile.GetFile(product.Path)));
			}
			return productGetBodies;
		}
		catch (Exception ex)
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



	[HttpPost]
	public async Task<ActionResult> Create([FromBody] ProductCreateBody body)
	{
		Product product = new()
		{
			Category = body.Category,
			CategoryId = body.Category.CategoryId,
			ImageFilePageData = new(body.ImageFile.FileName),
			Price = body.Price,
			Title = body.Title,
		};

		try
		{
			await _productContext.ImageFilesDatas.AddAsync(product.ImageFilePageData);
			await _productContext.Products.AddAsync(product);
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
