namespace H3GUIAPI.API.Models;

public class ImageFilePageData
{
	public int ImageFileId { get; set; } = 0;
	public string RelativePath { get; set; } = "";
	public Product Product { get; set; }

	public ImageFilePageData(Product product)
	{
		Product = product;

	}
}
