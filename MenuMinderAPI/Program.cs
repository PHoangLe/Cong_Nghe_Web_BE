using System.Text;
using AutoMapper;
using BusinessObjects.DataModels;
using MenuMinderAPI.Filters;
using MenuMinderAPI.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services;
using Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMvc(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
             .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Configure JWT scheama
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });


// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// builder.Services.AddScoped<GlobalExceptionMiddleware>();

// configure DI for application repositories
builder.Services.AddScoped<DiningTableRepository, DiningTableRepository>();
builder.Services.AddScoped<FoodRepository, FoodRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddScoped<PermissionRepository, PermissionRepository>();
builder.Services.AddScoped<AccountRepository, AccountRepository>();
builder.Services.AddScoped<PermitRepository, PermitRepository>();

// configure DI for DBContext
builder.Services.AddScoped<Menu_minder_dbContext, Menu_minder_dbContext>();

// configure DI for application repositories
builder.Services.AddScoped<IDiningTableRepository, DiningTableRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermitRepository, PermitRepository>();

// configure DI for application services
builder.Services.AddScoped<DiningTableService, DiningTableService>();
builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddScoped<JwtServices, JwtServices>();
builder.Services.AddScoped<FoodService, FoodService>();
builder.Services.AddScoped<CategoryService, CategoryService>();
builder.Services.AddScoped<AccountService, AccountService>();
builder.Services.AddScoped<PermissionService, PermissionService>();

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
