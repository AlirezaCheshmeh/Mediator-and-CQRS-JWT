

using mediatorCqrs.Application;
using mediatorCqrs.Application.Model.Identity;
using mediatorCqrs.Identity;
using mediatorCqrs.Persistence;
using mediatorCqrs.Persistence.Configuration.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigurationPersistenceService(builder.Configuration);
builder.Services.ConfigurationApplicationService();
builder.Services.AddIdentityService(builder.Configuration);

builder.Configuration.GetSection("JWTsetting").Get<JWTsetting>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UserSeedingData.Seed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
