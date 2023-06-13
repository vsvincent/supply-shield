using Microsoft.AspNetCore.Builder.Extensions;
using UserService.Models;
using UserService.Repository;
using UserService.Service;

var AllowSpecificOrigins = "_AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IUser, User>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService.Service.UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.AllowAnyOrigin();
                          //builder.WithOrigins("https://gateway-cmlmuykhqq-lm.a.run.app");
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCors(AllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
