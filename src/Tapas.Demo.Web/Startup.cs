﻿namespace Tapas.Demo.Web
{
    using System;
    using Data.EntityFramework;
    using ExtCore.WebApplication.Extensions;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly string extensionsPath;
        private readonly IHostingEnvironment hostingEnvironment;
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration, IHostingEnvironment hostingEnvironment )
        {
            this.hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            extensionsPath = hostingEnvironment.ContentRootPath + configuration[ "Tapas:Extensions:Path" ];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSingleton( Configuration )
                    .AddSingleton( hostingEnvironment );

            services.Configure<CookiePolicyOptions>( options =>
                                                     {
                                                         // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                                         options.CheckConsentNeeded = context => true;
                                                         options.MinimumSameSitePolicy = SameSiteMode.None;
                                                     } );

            services.AddRouting( x => x.LowercaseUrls = true )
                    .AddMvc( options =>
                             {
                                 var policy = new AuthorizationPolicyBuilder()
                                              .RequireAuthenticatedUser()
                                              .Build();

                                 options.Filters.Add( new AuthorizeFilter( policy ) );
                             } )
                    .SetCompatibilityVersion( CompatibilityVersion.Version_2_1 )
                    .AddFluentValidation();

            services.AddExtCore( extensionsPath );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider )
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
            app.UseMvcWithDefaultRoute();
            app.UseExtCore();
            serviceProvider.GetService<ApplicationDbContext>()
                           ?.Database
                           ?.Migrate();
        }
    }
}