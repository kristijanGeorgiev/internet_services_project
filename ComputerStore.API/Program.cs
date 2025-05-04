using Microsoft.EntityFrameworkCore;
using ComputerStore.API.Mappings;
using ComputerStore.Application.Interfaces;
using ComputerStore.Application.Services;
using ComputerStore.Infrastructure.Data;
using ComputerStore.Infrastructure.Repositories;
using ComputerStore.Infrastructure.Services;
using Infrastructure.Services;
using ComputerStore.API.Middleware; // If you have other Core services (optional)

var builder = WebApplication.CreateBuilder(args);

// Register services into the container

// Register DbContext (Connection String from appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register Infrastructure and Application Services
builder.Services.AddScoped<IStockImportService, StockImportService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDiscountCalculatorService, DiscountCalculatorService>();
builder.Services.AddScoped<JsonLoaderService>();

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Computer Store API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Computer Store API v1");
    });
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Required for WebApplicationFactory<T> in Tests
public partial class Program { }