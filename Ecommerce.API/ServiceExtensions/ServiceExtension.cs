using Ecommerce.API.AutoMapper;
using Ecommerce.Business.Models;
using Ecommerce.Business.PipelineBehaviors;
using Ecommerce.Business.Services;
using Ecommerce.Core.DTOs.Settings;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace Ecommerce.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };             
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITokenService, TokenService>();
            //Add scope services here
            return services;
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcommerceDbContext>(opts => opts.UseNpgsql(configuration.GetConnectionString("sqlConnection")));
        }
        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void ConfigMediatR(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Response<>).Assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(Response<>).Assembly);
                cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });
        }

        public static void ConfigOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JwtSettings"));
        }
        public static void ConfigSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 123456abcd')",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
        public static void ConfigAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();

        }
    }
}
