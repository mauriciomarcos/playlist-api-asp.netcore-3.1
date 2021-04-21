using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Playlist.API.Data.Context;

namespace Playlist.API.Configurations
{
    public static class EntityFrameworkCoreConfig
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(param => param.AddConsole());

        public static IServiceCollection AdicionarConfiguracaoDoEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<PlayListDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PlayListDbContext>(options => {
                options
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("dbPlayList");
            });

            return services;
        }
    }
}