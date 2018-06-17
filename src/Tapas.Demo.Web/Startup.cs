namespace Tapas.Demo.Web
{
    using ExtCore.WebApplication.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public string extensionsPath { get; set; }

        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration, IHostingEnvironment hostingEnvironment )
        {
            Configuration = configuration;
            extensionsPath = hostingEnvironment.ContentRootPath + configuration[ "Extensions:Path" ];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.Configure<CookiePolicyOptions>( options =>
                                                     {
                                                         // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                                         options.CheckConsentNeeded = context => true;
                                                         options.MinimumSameSitePolicy = SameSiteMode.None;
                                                     } );

            services.AddExtCore( extensionsPath );
            services.AddMvc()
                    .SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Home/Error" );
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseExtCore();
            app.UseMvc( routes =>
                        {
                            routes.MapRoute(
                                            "default",
                                            "{controller=Home}/{action=Index}/{id?}" );
                        } );
        }
    }
}