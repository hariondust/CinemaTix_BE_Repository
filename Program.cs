using CinemaTix.Data;
using CinemaTix.Data.Seeds;
using CinemaTix.Interfaces;
using CinemaTix.Middleware;
using CinemaTix.Repositories;
using CinemaTix.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            .UseLazyLoadingProxies()
);

// Register Repository
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Register Service
builder.Services.AddSingleton<JWTServices>();
builder.Services.AddScoped<IMovieService, MovieServices>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IOrderService, OrderServices>();
builder.Services.AddScoped<IShowService, ShowServices>();
builder.Services.AddScoped<IReviewService, ReviewServices>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();


// Configure Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "CinemaTix";
});

var issuer = builder.Configuration["JWTSettings:Issuer"];
var audience = builder.Configuration["JWTSettings:Audience"];
var issuerKey = builder.Configuration["JWTSettings:SecretKey"];

// Authentication Process
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerKey!))
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinematix API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your token directly in the text input below without the word 'Bearer'.\nExample: <your-jwt-token>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (args.Contains("UsersSeed") || args.Contains("MoviesSeed") || args.Contains("ShowsSeed"))
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (args.Contains("UsersSeed")) await UsersSeed.SeedUsers(context);
        if (args.Contains("MoviesSeed")) await MoviesSeed.SeedMovies(context);
        if (args.Contains("ShowsSeed")) await ShowsSeed.SeedShows(context);
    }
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<UserMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
