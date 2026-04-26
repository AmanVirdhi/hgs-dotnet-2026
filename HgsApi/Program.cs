using Microsoft.EntityFrameworkCore;
using HgsApi.Data;

var builder = WebApplication.CreateBuilder(args);

//to conntect with cross origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "HgsApi");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); //for CORS Connection

app.UseAuthorization();

app.MapControllers();

app.Run();
