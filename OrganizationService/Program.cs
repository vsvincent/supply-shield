using Amazon.QLDB.Driver;
using OrganizationService.Models;
using OrganizationService.Repository;
using OrganizationService.Services;
using OrganizationService.Utils;

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\01\\.ssh\\supply-shield-381721-c0ef0545d29b.json");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrganizationService, OrganizationService.Services.OrganizationService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganization, Organization>();
builder.Services.AddScoped<IQldbContext, QldbContext>();
builder.Services.AddScoped<IGoogleSecretManager, GoogleSecretManager>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();