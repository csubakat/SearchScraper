using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;
using SearchScraper.Modules.Clients;
using SearchScraper.Modules.Factories;
using SearchScraper.Modules.SearchEngines;
using SearchScraper.Services;

namespace SearchScraper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<ISearchEngineProviderFactory, SearchEngineProviderFactory>();
            services.AddSingleton<IScrapingService, ScrapingService>();
            services.AddSingleton<IWebClient, SystemWebClient>();


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            services.AddTransient<GoogleEngine>(s =>
            {
                var settings = new SearchEngineProviderSettingsResolver(SearchEngine.Google, configuration)
                    .GetSettings();
                return new GoogleEngine(settings, new WebClientFactory());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
