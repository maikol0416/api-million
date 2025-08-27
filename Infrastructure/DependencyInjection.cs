using Domain.Entities;
using System.Text;
using Domain.Port;
using Infrastructure.Integration;
using Infrastructure.Repository;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infraestructure;
// using Infraestructure;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MainContextSQLServer>(options => options.UseSqlServer(configuration[$"{nameof(ConfigurateSQLServer)}:{nameof(ConfigurateSQLServer.ConnectionString)}"], b => b.MigrationsAssembly("Api")));
            services.AddSingleton<IConfigurateSQLServer>(sp => sp.GetRequiredService<IOptions<ConfigurateSQLServer>>().Value);
            services.AddScoped<IMainContextSQLServer, MainContextSQLServer>();

            services.AddScoped(typeof(ITestRepository), typeof(TestRepository));

            services.AddScoped(typeof(IOwnerRepository), typeof(OwnerRepository));
            services.AddScoped(typeof(IPropertyRepository), typeof(PropertyRepository));
            services.AddScoped(typeof(IPropertyTraceRepository), typeof(PropertyTraceRepository));
            services.AddScoped(typeof(IPropertyImageRepository), typeof(PropertyImageRepository));
            services.AddTransient<IAzureStorage, AzureStorage>();

            #region JWT-identity-sqlserver

            services.AddIdentity<UserApplication, IdentityRole>()
                .AddEntityFrameworkStores<MainContextSQLServer>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };
            });
            #endregion

            return services;
        }
    }
}
