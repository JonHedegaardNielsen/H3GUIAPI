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

	[HttpGet("FromCategoryId/{categoryId}")]
	public async Task<IEnumerable<ProductGetBody>> GetFromCategaory(int categoryId)
	{
		try
		{
			var products = _productContext.Products.Where((p) => p.Category.CategoryId == categoryId)
			.Select((p) =>
				new ProductGetBody(p.ProductId, p.Title, p.Category, p.Price))
				.ToArray();
			return products;
		}
		catch
		{
			return [];
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ProductGetBody>> GetSingle(uint id)
	{
		try
		{

			var product = await _productContext.Products.Include(p => p.Category)
				.Select((p) =>
					new
					{
						p.ProductId,
						p.Title,
						p.Category,
						p.Price,
						Path = p.ImageFilePageData.RelativePath
					}
				)
				.FirstOrDefaultAsync(p => p.ProductId == id);

			ProductGetBody productGetBody = new ProductGetBody(product.ProductId, product.Title, product.Category, product.Price, await ImageFile.GetFile(product.Path));
			return Ok(productGetBody);
		}
		catch
		{
			return NotFound();
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
