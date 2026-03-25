using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdminAPI.Data;
using AdminAPI.Features.Auth.Services;
using AdminAPI.Features.Admins.Services;
using AdminAPI.Features.Roles.Services;
using AdminAPI.Features.Permissions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

// Configure MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
        )
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add authorization policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin:list", policy => policy.RequireClaim("permission", "admin:list"))
    .AddPolicy("admin:detail", policy => policy.RequireClaim("permission", "admin:detail"))
    .AddPolicy("admin:create", policy => policy.RequireClaim("permission", "admin:create"))
    .AddPolicy("admin:update", policy => policy.RequireClaim("permission", "admin:update"))
    .AddPolicy("admin:delete", policy => policy.RequireClaim("permission", "admin:delete"))
    .AddPolicy("admin:status", policy => policy.RequireClaim("permission", "admin:status"))
    .AddPolicy("admin:reset-pwd", policy => policy.RequireClaim("permission", "admin:reset-pwd"))
    .AddPolicy("role:list", policy => policy.RequireClaim("permission", "role:list"))
    .AddPolicy("role:detail", policy => policy.RequireClaim("permission", "role:detail"))
    .AddPolicy("role:create", policy => policy.RequireClaim("permission", "role:create"))
    .AddPolicy("role:update", policy => policy.RequireClaim("permission", "role:update"))
    .AddPolicy("role:delete", policy => policy.RequireClaim("permission", "role:delete"))
    .AddPolicy("role:assign", policy => policy.RequireClaim("permission", "role:assign"))
    .AddPolicy("permission:list", policy => policy.RequireClaim("permission", "permission:list"))
    .AddPolicy("permission:detail", policy => policy.RequireClaim("permission", "permission:detail"))
    .AddPolicy("permission:create", policy => policy.RequireClaim("permission", "permission:create"))
    .AddPolicy("permission:update", policy => policy.RequireClaim("permission", "permission:update"))
    .AddPolicy("permission:delete", policy => policy.RequireClaim("permission", "permission:delete"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run("http://localhost:9001");
