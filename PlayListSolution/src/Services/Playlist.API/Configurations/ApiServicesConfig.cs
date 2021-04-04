using Microsoft.Extensions.DependencyInjection;
using Playlist.API.Data.Repository;
using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.Domain.Services;
using Playlist.API.ViewModels;

namespace Playlist.API.Configurations
{
    public static class ApiServicesConfig
    {
        public static IServiceCollection AdicionarConfiguracaoInjecaoDependenciaAplicacao(this IServiceCollection services)
        {
            services.AddScoped<IVideoService<VideoViewModel>, VideoService>();
            services.AddScoped<IVideoRepository, VideoRepository>();

            return services;
        }

        public static IServiceCollection AdicionarConfiguracaoCORS(this IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy("cross-site-allow", policy =>
            {
                policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            return services;
        }
    }
}