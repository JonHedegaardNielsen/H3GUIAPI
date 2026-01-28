namespace H3GUIAPI.API.Models;


public class Category
{
	public string Title { get; set; } = "";
	public uint CategoryId { get; set; } = 0;
	public Category(string title = "", uint categoryId = 0)
	{
		Title = title;
		CategoryId = categoryId;
	}
}
