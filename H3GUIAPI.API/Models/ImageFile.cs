using H3GUIAPI.API.Models;

public class ImageFile
{
	public ImageFile(string contentBase64, string fileName)
	{
		ContentBase64 = contentBase64;
		FileName = fileName;
	}

	public string ContentBase64 { get; set; } = "";
	public string FileName { get; set; } = "";

	static string FolderPath
	{
		get
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			string folderPath = System.IO.Path.Join(path, "APIImageFiles");
			if (Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}
			return folderPath;
		}
	}

	public void SaveFile()
	{
		using FileStream fileStream = File.Create(Path.Join(FolderPath, FileName));
		fileStream.Write(Convert.FromBase64String(ContentBase64));
	}

	public static async Task<ImageFile> GetFile(string fileName)
	{
		string data = File.ReadAllText(Path.Join(FolderPath, fileName));
		return new ImageFile(data, fileName);
	}
}
