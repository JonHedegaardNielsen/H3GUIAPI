using H3GUIAPI.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductContext>(opt => opt.UseSqlite("TodoList"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
	config.DocumentName = "TodoAPI";
	config.Title = "TodoAPI v1";
	config.Version = "v1";
});

builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAllOrigins", policy =>
					{
						policy.SetIsOriginAllowed((host) => true)
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
					});
		});
var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
	app.UseOpenApi();
	app.UseSwaggerUi(config =>
	{
		config.DocumentTitle = "TodoAPI";
		config.Path = "/swagger";
		config.DocumentPath = "/swagger/{documentName}/swagger.json";
		config.DocExpansion = "list";
	});
}
app.MapControllers();

app.Run();
