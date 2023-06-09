using IncidentService.Models;
using IncidentService.Repository;
using IncidentService.Service;
using IncidentService.Utils;

var AllowSpecificOrigins = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIncidentService, IncidentService.Service.IncidentService>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IIncident, Incident>();
builder.Services.AddScoped<IGoogleSecretManager, GoogleSecretManager>();
builder.Services.AddScoped<IQldbContext, QldbContext>();

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

app.UseCors(AllowSpecificOrigins);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

CommunicationService.Initialize();

app.Run();
