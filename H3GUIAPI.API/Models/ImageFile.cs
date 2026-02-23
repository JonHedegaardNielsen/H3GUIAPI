using System.Net.Http.Headers;
using H3GUIAPI.API.Models;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Buffers.Text;
using System.Security.Cryptography;
namespace H3GUIAPI.API.Models;

public record ImageFile(string FileName, string ContentBase64)
{
	static string FolderPath
	{
		get
		{
			if (field is not null)
			{
				return field;
			}
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			string folderPath = Path.Join(path, "APIImageFiles");
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
			field = folderPath;
			return field;
		}
	}

	public static async Task<bool> Save(ImageFile file)
	{
		string path = Path.Combine(FolderPath, file.FileName);
		try
		{
			using StreamWriter streamWriter = new(path);
			var b = Convert.FromBase64String(file.ContentBase64);
			await File.WriteAllBytesAsync(path, b);
		}
		catch (Exception ex)
		{
			return false;
		}
		return true;
	}

	public static async Task<ImageFile> GetFile(string fileName)
	{
		string path = Path.Combine(FolderPath, fileName);
		if (!File.Exists(path))
		{
			throw new FileLoadException("File Not Found");
		}

		var fileBytes = File.ReadAllBytes(path);
		return new ImageFile(fileName, Convert.ToBase64String(fileBytes));
	}
}
