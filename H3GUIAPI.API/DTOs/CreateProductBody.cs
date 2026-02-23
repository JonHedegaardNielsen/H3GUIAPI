using H3GUIAPI.API.Models;

namespace H3GUIAPI.API.DTOs;

public record ProductCreateBody(string Title, Category Category, decimal Price, ImageFile ImageFile);
public record ProductGetBody(int ProductId, string Title, Category Category, decimal Price, ImageFile? ImageFile = null);
