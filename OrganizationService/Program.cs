using Amazon.QLDB.Driver;
using OrganizationService.Models;
using OrganizationService.Repository;
using OrganizationService.Services;
using OrganizationService.Utils;

var AllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrganizationService, OrganizationService.Services.OrganizationService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganization, Organization>();
builder.Services.AddScoped<IQldbContext, QldbContext>();
builder.Services.AddScoped<IGoogleSecretManager, GoogleSecretManager>();

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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(AllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();