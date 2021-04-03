using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playlist.API.Data.Context;

namespace Playlist.API.Configurations
{
    public static class EntityFrameworkCoreConfig
    {
        public static IServiceCollection AdicionarConfiguracaoDoEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<PlayListDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PlayListDbContext>(options =>
                options.UseInMemoryDatabase("dbPlayList"));

            return services;
        }
    }
}