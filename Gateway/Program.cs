using FirebaseAdmin;
using Gateway.Extensions;
using Gateway.Services;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();

builder.Services.AddControllers();

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.GetApplicationDefault(),
    ProjectId = "supply-shield-381721"
}));
builder.Services.AddFirebaseAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          //builder.AllowAnyOrigin();
                          builder.WithOrigins("https://supply-shield-381721.web.app",
                                             "https://supply-shield-381721.firebaseapp.com");
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
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
