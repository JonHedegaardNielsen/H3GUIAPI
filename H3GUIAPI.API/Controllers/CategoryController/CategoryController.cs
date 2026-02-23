using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3GUIAPI.API.Models
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		ProductContext _productContext;

		public CategoryController(ProductContext productContext)
		{
			_productContext = productContext;
		}
		[HttpGet]
		public IEnumerable<Category> GetAll()
		{
			try
			{
				return _productContext.Categories.ToArray();
			}
			catch
			{
				return [];
			}
		}
		[HttpGet("{id}")]
		public ActionResult<Category> GetAll(uint id)
		{
			try
			{
				return Ok(_productContext.Categories.Single(c => c.CategoryId == id));
			}
			catch
			{
				return NotFound();
			}
		}
	}
}
