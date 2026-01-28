namespace H3GUIAPI.API.Models;

public class Product
{
	public uint ProductId { get; set; } = 0;
	public string Title { get; set; } = "";
	public decimal Price { get; set; } = 0;
	public string ImageURL { get; set; } = "";
	public uint CategoryId { get; set; } = 0;
	public Category Category { get; set; }

}
