namespace H3GUIAPI.API.Models;

public class ImageFilePageData
{
	public int ImageFilePathDataId { get; set; } = 0;
	public string RelativePath { get; set; } = "";
	public ImageFilePageData(string relativePath, int ImageFilePathDataId = 0)
	{
		this.ImageFilePathDataId = ImageFilePathDataId;
		RelativePath = relativePath;
	}
}
