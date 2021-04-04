using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playlist.API.Configurations;

namespace Playlist.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {          
            services.AddControllers();
            services.AdicionarConfiguracaoCORS();
            services.AdicionarConfiguracaoDoEntityFramework(Configuration);
            services.AdicionarConfiguracaoDoSwagger();
            services.AdicionarConfiguracaoInjecaoDependenciaAplicacao();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsarConfiguracaoDoSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cross-site-allow");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}