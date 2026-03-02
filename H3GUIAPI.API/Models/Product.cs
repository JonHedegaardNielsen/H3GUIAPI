namespace H3GUIAPI.API.Models;

public class Product
{
	public int ProductId { get; set; } = 0;
	public string Title { get; set; } = "";
	public decimal Price { get; set; } = 0;
	public uint CategoryId { get; set; } = 0;
	public Category? Category { get; set; }
	public ImageFilePageData? ImageFilePageData { get; set; }
	public int ImageFilePageDataId { get; set; }

}
