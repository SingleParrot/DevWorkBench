using System.Text;
using DevWorkBench;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class Program 
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi();
        // MVC = Model View Controller
        // Addding Services 
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        // Add Cors settings 
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactFrontend",policy =>
            {
                policy.WithOrigins("https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });

        // JWT 
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
                    ValidAudience = builder.Configuration["Jwt:Audience"]!,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)
                    )
                };
            });
        
        // EF Core Contexts
        builder.Services.AddDbContext<UserContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"))
        );

        builder.Services.AddDbContext<JwtContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"))
        );

        builder.Services.AddDbContext<RoleContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"))
        );


        // Auth Services for Reistration and Login
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<TokenService>();
        builder.Services.AddScoped<AuthRoleService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); // Swagger UI for API documentation
        }

        app.UseCors("AllowReactFrontend");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        await app.RunAsync(); // dotnet run --launch-profile https  
        FileLogger logger = FileLogger.Instance;
        logger.CreateLogFile();
        logger.AddLog("Server started","INFO");
        ConsoleLogger.Info("Server started");
    }
}