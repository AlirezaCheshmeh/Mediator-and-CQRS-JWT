

using mediatorCqrs.Application;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Identity;
using mediatorCqrs.Persistence;
using mediatorCqrs.Persistence.Configuration.Entities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme , e.g \"bearer {Token}\"" , 
        In =ParameterLocation.Header ,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.ConfigurationPersistenceService(builder.Configuration);
builder.Services.ConfigurationApplicationService();
builder.Services.AddIdentityService(builder.Configuration);

builder.Configuration.GetSection("JWTsetting:key").Get<JWTsetting>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UserSeedingData.Seed(app);

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
