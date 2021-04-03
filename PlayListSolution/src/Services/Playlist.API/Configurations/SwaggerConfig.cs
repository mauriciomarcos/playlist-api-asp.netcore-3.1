using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Playlist.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AdicionarConfiguracaoDoSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Playlist API",
                    Description = "API que expões os endpoints para consumo externo.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Maurício Marcos",
                        Email = "001.mmarcos@gmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licences/MIT")
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UsarConfiguracaoDoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
            });

            return app;
        }
    }
}