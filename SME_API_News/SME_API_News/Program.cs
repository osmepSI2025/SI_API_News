
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using SME_API_News.Entities;
using SME_API_News.Repository;
using SME_API_News.Service;
using SME_API_News.Services;
using System.Text;

namespace SME_API_News
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(
                   path: "Logs\\log-.txt",
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:1j}{NewLine}{Exception}",
                   rollingInterval: RollingInterval.Day,
                   restrictedToMinimumLevel: LogEventLevel.Information
               ).CreateLogger();
            try
            {

                Log.Information("Starting web application");
                var builder = WebApplication.CreateBuilder(args);
                //Jwt configuration starts here
                var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
                var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = jwtIssuer,
                         ValidAudience = jwtIssuer,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                     };
                 });
                //Jwt configuration ends here
                ConfigurationManager configuration = builder.Configuration;
                // Add services to the container.
                builder.Services.AddScoped<INewsRepository, NewsRepository>();
                builder.Services.AddScoped<IDropdownRepository, DropdownRepository>();
                builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
                builder.Services.AddScoped<IMPopupRepository, MPopupRepository>();
                builder.Services.AddScoped<IBannerRepository, BannerRepository>();
                builder.Services.AddScoped<UserManagementRepository>();
                builder.Services.AddScoped< UserManagementService>();
                builder.Services.AddScoped<HrEmployeeService>();
                builder.Services.AddScoped<IApiInformationRepository, ApiInformationRepository>();
                builder.Services.AddScoped<ICallAPIService, CallAPIService>(); // Register ICallAPIService with CallAPIService
                builder.Services.AddHttpClient<CallAPIService>();
                var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
                builder.Services.AddCors(options =>
                {
                    string allowCORS = configuration["AppSettings:AllowCORS"];
                    string allowCORS2 = configuration["AppSettings:AllowCORS2"];
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                                          policy =>
                                          {
                                              policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                              //policy.WithOrigins(allowCORS, allowCORS2).AllowAnyHeader().AllowAnyMethod();
                                          });
                });


                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                //builder.Services.AddSwaggerGen();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "Bearer Authentication with JWT Token",
                        Type = SecuritySchemeType.Http
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
                });
                builder.Services.AddDbContext<NewsDBContext>(options =>
                {
                    options.UseSqlServer(configuration["AppSettings:ConnectionString"]);
                    //              builder.Services.AddDbContext<DBContext>(options =>
                    //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

                });
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
