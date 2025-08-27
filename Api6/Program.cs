using Api.Common.MiddleException;
using Api.Installers;
using Api6.Common;
using BlobStorage;
using BlobStorage.Interface;
using Domain.Entities;
using Domain.Port;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Integration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceApplication;
using System.Text;
using Util.Common;
using Utilidades;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<MiddleHandlerException>();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(Constants.MyAllowSpecificOrigins);

app.Run();

void ConfigureServices(IServiceCollection services)
{

    //this.Sql(services);
    services.AddResponseCompression();
    services.AddHttpContextAccessor();

    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient<IUtil, Utilities>();
    //services.AddTransient<IServicesBusHandler, ServiceSenderHandler>();    


    services.AddDependencyInjectionsInfrastructure(builder.Configuration);
    services.AddDependencyInjectionsApplications();
    services.AddMediatrDependecyInjection();

    services.AddAuthorization();
    services.AddControllers();


    ConfiguracionBase(services);
}

void ConfiguracionBase(IServiceCollection services)
{


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();


    #region swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(ConstantsAPI.VersionProject,new OpenApiInfo
        {
            Title = ConstantsAPI.NameProject,
            Version = ConstantsAPI.VersionProject,
            Description = ConstantsAPI.DescriptionProject,
            Contact = new OpenApiContact
            {
                Email = ContactProject.Email,
                Name = ContactProject.Name
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "Bearer {token}",
            In = ParameterLocation.Header,
            Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        });
        c.OperationFilter<RequiredHeaderParameter>();
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
                                new string[] {}
                            }
                    });
    });
    #endregion

    #region Cors
    services.AddCors(options =>
    {
        options.AddPolicy(name: Constants.MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://barbershop-pdn-401.azureedge.net")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                          });
    });

    #endregion

}


