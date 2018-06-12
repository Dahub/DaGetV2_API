using AspNet.Security.OAuth.Introspection;
using DaGetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DaGetCore.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));
            services.AddLogging();
            services.AddMvc();

            var conf = Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
            }).AddOAuthIntrospection(options =>
            {
                options.Configuration = new OAuthIntrospectionConfiguration()
                {
                    IntrospectionEndpoint = "http://localhost:63293/introspect"
                };

                options.IncludeErrorDetails = true;
                options.Audiences.Add(conf.Audience);
                options.ClientId = conf.Login;
                options.ClientSecret = conf.ServerSecret;
                options.RequireHttpsMetadata = conf.RequireHttps;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateBankAccount",
                    policy =>
                    {
                        policy.Requirements.Add(new HaveScopeRequirement("daget:bankaccount:write"));                        
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            factory.AddConsole(LogLevel.Information);
            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
