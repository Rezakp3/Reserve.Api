using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Helpers;
using Infrastructure;
using Application;
using Application.Authentication.Policy;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IAuthorizationHandler, JustActiveAuth>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerService();
builder.Services.AddJWTAuthentication(builder.Configuration);

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("JustActive", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("IsActive");
        policy.Requirements.Add(new IsActiveRequirement());
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }