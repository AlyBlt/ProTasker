using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Mapping;
using ProTasker.Application.Models;
using ProTasker.Application.Services;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using ProTasker.Infrastructure.Data;
using ProTasker.Infrastructure.Repositories;
using System.Data;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Identity için yapýlandýrma
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT Key kontrolü (önce)
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT key is missing in configuration.");

// JWT Authentication yapýlandýrmasý
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddAuthorization(options =>
{
    // Admin rolü için policy
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    // Member rolü için policy
    options.AddPolicy("MemberOnly", policy => policy.RequireRole("Member"));
    //TeamLeader içimn policy
    options.AddPolicy("TeamLeaderOnly", policy => policy.RequireRole("TeamLeader"));
});


// Repository
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
builder.Services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();

// Service
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<ITaskHistoryService, TaskHistoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(ProTasker.Application.Mapping.MappingProfile));

//Add Controllers
builder.Services.AddControllers();

// Add Swagger for testing
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProTasker API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token."
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Veritabanýný otomatik olarak güncelle //geliþtirme aþamasýnda kalabilir migration daha sonra kaldýracaðým
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    // Rolleri oluþtur
    string[] roleNames = { "Admin", "TeamLeader", "Member" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        }
    }

    // Admin kullanýcýyý oluþtur
    var admin = await userManager.FindByEmailAsync("admin@protasker.com");
    if (admin == null)
    {
        admin = new ApplicationUser
        {
            UserName = "Admin",
            Email = "admin@protasker.com",
            Role = UserRole.Admin
        };

        var createResult = await userManager.CreateAsync(admin, "Admin123!");
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
    else
    {
        // Eðer admin zaten varsa ama security stamp null ise düzelt
        if (string.IsNullOrEmpty(admin.SecurityStamp))
        {
            admin.SecurityStamp = Guid.NewGuid().ToString();
            await userManager.UpdateAsync(admin);
        }

        // Eðer admin rolde deðilse ekle
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}

// 5. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Authentication ve Authorization middleware'larýný ekleme
app.UseAuthentication();
app.UseAuthorization();

// Controller'larý route'lama
app.MapControllers();

app.Run();
