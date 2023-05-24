using Amazon.QLDB.Driver;
using OrganizationService.Models;
using OrganizationService.Repository;
using OrganizationService.Services;
using OrganizationService.Utils;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrganizationService, OrganizationService.Services.OrganizationService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganization, Organization>();
builder.Services.AddScoped<IQldbContext, QldbContext>();
builder.Services.AddScoped<IGoogleSecretManager, GoogleSecretManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.WithOrigins("*",
                                              "https://supply-shield-381721.web.app",
                                              "https://supply-shield-381721.firebaseapp.com");

                      });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();