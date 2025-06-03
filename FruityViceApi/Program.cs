using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger; // Add this using directive for Swagger support

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FruityVice API", Version = "v1" });
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
app.UseSwagger(); // This requires the Swashbuckle.AspNetCore NuGet package
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruityVice API v1"));

app.Run();
