using Application.Interfaces;
using Application.Services;
using Domain.Entity;
using Domain.Ports;
using Infraestructure;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExcelImportService, ExcelImportService>();
builder.Services.AddScoped<IOvertimeService, OvertimeService>();
builder.Services.AddScoped<IOvertimeRepository, OvertimeRepository>();
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtKey = builder.Configuration["CredentialsJwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured properly.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["CredentialsJwt:Issuer"],
                ValidAudience = builder.Configuration["CredentialsJwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["CredentialsJwt:Key"]))
            };
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanOvertimeRoles", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(Role.Assitant.ToString(), Role.Leader.ToString(), Role.Manager.ToString());
    });

    options.AddPolicy("CanEmployeeRoles", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(Role.Assitant.ToString(), Role.Leader.ToString(), Role.Manager.ToString(), Role.Employee.ToString());
    });

    options.AddPolicy("CanApproveOvertime", policy =>
    {
        policy.RequireRole(Role.Leader.ToString(), Role.Manager.ToString());
        policy.RequireClaim("Permission", Permission.ApproveOvertime);
    });

    options.AddPolicy(Role.Assitant.ToString(), policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(Role.Assitant.ToString());
    });

    options.AddPolicy(Role.Leader.ToString(), policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(Role.Leader.ToString());
    });

    options.AddPolicy(Role.Employee.ToString(), policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(Role.Employee.ToString());
    });
});

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