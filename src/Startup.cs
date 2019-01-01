using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitAttributesWeb.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NWebsec.AspNetCore.Middleware;

namespace GitAttributesWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add MVC services to the services container.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<MvcOptions>(options =>
            {
                options.OutputFormatters.Clear();

                var textOutput = new StringOutputFormatter2();
                options.OutputFormatters.Add(textOutput);
            });

            services.AddSingleton<AppData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // configure Content Security Policy policy
            app.UseCsp(options =>
            {
                options.DefaultSources(s => s.Self());
                options.StyleSources(s => s.Self().CustomSources("fonts.googleapis.com"));
                options.ScriptSources(s => s.Self().CustomSources("code.jquery.com"));
                options.FontSources(s => s.Self().CustomSources("fonts.googleapis.com", "fonts.gstatic.com"));

                options.ReportUris(s => s.Uris("https://goit.report-uri.io/r/default/csp/enforce"));
            });

            // configure X-Content-Type-Options policy
            app.UseXContentTypeOptions();

            // configure X-Frame-Options policy
            app.UseXfo(options => options.Deny());

            // configure X-XSS-Protection policy
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // configure HTTP Strict Transport Security policy
                app.UseHsts(options =>
                {
                    options.MaxAge(days: 30).IncludeSubdomains();
                });
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
