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

            var appSettingsConfig = CreateConfiguration("appsettings.json");

            services.AddTransient<GoogleEngine>(s =>
            {
                var settings = new SearchEngineProviderSettingsResolver(SearchEngine.Google, appSettingsConfig)
                    .GetSettings();
                return new GoogleEngine(settings, new WebClientFactory());
            });
            services.AddTransient<BingEngine>(s =>
            {
                var settings = new SearchEngineProviderSettingsResolver(SearchEngine.Bing, appSettingsConfig)
                    .GetSettings();
                return new BingEngine(settings, new WebClientFactory());
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

        private IConfiguration CreateConfiguration(string settingsFileName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingsFileName);

            return builder.Build();
        }
    }
}
