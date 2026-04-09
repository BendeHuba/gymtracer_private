
using GymTracer.Auth;
using GymTracer.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GymTracer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var configuration = builder.Configuration;
            var connString = configuration.GetConnectionString("gymtracerDb");
            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200") // TODO: kubernetes frontend project ip hozzáadása
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(AddSwaggerAuthButton);
            builder.Services.AddDbContext<GymTracerDbContext>(o =>
            {
                o.UseMySQL(connString);
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "MyAuthentication";

            }).AddScheme<AuthOptions, AuthHandler>("MyAuthentication", options =>
            {
                builder.Configuration.GetSection(AuthOptions.SectionName).Bind(options);
            }).AddBearerToken();

            builder.Services.AddAuthorization(options =>
            {
                var sessionTokenPolicy = new AuthorizationPolicyBuilder().RequireClaim("SessionToken").Build();

                options.AddPolicy("SessionToken", sessionTokenPolicy);


                options.DefaultPolicy = sessionTokenPolicy;
            });

            builder.Services.Configure<AuthOptions>(
                builder.Configuration.GetSection(AuthOptions.SectionName));

            builder.Services.AddSingleton<TokenHandler>();

            builder.Services.Configure<PasswordOptions>(
                builder.Configuration.GetSection(PasswordOptions.SectionName));
            builder.Services.AddSingleton<PasswordHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddSwaggerAuthButton(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Unique Bearer Token API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "Unique",
                In = ParameterLocation.Header,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        }
    }
}
