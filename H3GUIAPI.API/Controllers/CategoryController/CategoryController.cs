using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H3GUIAPI.API.Models
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<Category> GetAll()
		{
			try
			{
				using ProductContext context = new();
				return context.Categories.ToArray();
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
				using ProductContext context = new();
				return Ok(context.Categories.Single(c => c.CategoryId == id));
			}
			catch
			{
				return NotFound();
			}
		}
	}
}
