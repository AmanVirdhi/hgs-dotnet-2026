using Microsoft.EntityFrameworkCore;
using HgsApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 🔥 FIX for Render HTTPS
app.Use(async (context, next) =>
{
    context.Request.Scheme = "https";
    await next();
});

app.MapOpenApi();

// 🔥 Swagger UI always enabled
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "HgsApi");
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// root endpoint
app.MapGet("/", () => "API is running 🚀");

app.Run();