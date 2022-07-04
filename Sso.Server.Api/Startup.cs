using Common.API;
using Common.Domain.Base;
using Common.Domain.Model;
using IdentityServer4;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Seed.CrossCuting;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sso.Server.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
        
            var cns =
             Configuration
                .GetSection("ConfigConnectionString:Default").Value;


            services.AddIdentityServer(optionsConfig());
                //.AddSigningCredential(GetRSAParameters())
                //.AddCustomTokenRequestValidator<ClientCredentialRequestValidator>()
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryApiScopes(Config.GetApiScopes())
                //.AddInMemoryClients(Config.GetClients(Configuration.GetSection("ConfigSettings").Get<ConfigSettingsBase>()));

            //Configurations
            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.Configure<ConfigConnectionStringBase>(Configuration.GetSection("ConfigConnectionString"));
            
            //Container DI
            services.AddScoped<CurrentUser>();
            services.AddScoped<IUserCredentialServices, UserCredentialServices>();
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddScoped<ICustomTokenRequestValidator, ClientCredentialRequestValidator>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "857854978384-sv33ngtei50k8fn5ea37rcddo08n0ior.apps.googleusercontent.com";
                options.ClientSecret = "x1SWT89gyn5LLLyMNFxEx_Ss";
            });

            // Add cross-origin resource sharing services Configurations
            var configuration = new ConfigSettingsBase();
            Configuration.GetSection("ConfigSettings").Bind(configuration);
            Cors.Enable(services, configuration.ClientAuthorityEndPoint.ToArray());

            services.AddControllersWithViews();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            loggerFactory.AddFile(Configuration.GetSection("Logging"));
            
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None
            });
            app.UseCors("AllowStackOrigin");
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.AddTokenMiddlewareCustom();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        
        private Action<IdentityServer4.Configuration.IdentityServerOptions> optionsConfig()
        {
            return options => { options.IssuerUri = Configuration.GetSection("ConfigSettings:IssuerUri").Value; };
        }
        
        private X509Certificate2 GetRSAParameters()
        {
            var fileCert = Path.Combine(_env.ContentRootPath, "pfx", "ids4smbasic.pfx");
            if (!File.Exists(fileCert))
                throw new InvalidOperationException("Certificado não encontrado");

            var password = "vm123s456";
            return new X509Certificate2(fileCert, password, X509KeyStorageFlags.Exportable);
        }
    }
}
