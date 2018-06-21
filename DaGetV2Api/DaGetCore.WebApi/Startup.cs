using AspNet.Security.OAuth.Introspection;
using DaGetCore.Dal.EF;
using DaGetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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

            services.AddTransient<IBankAccountService>(c => new BankAccountService()
            {
                Factory = new EfRepositoriesFactory(),
                ConnexionString = Configuration.GetConnectionString("DaGetConnexionString")
            });

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
                    IntrospectionEndpoint = conf.DaOAuthIntrospectUri.ToString()
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
                options.AddPolicy("UpdateBankAccount",
                   policy =>
                   {
                       policy.Requirements.Add(new HaveScopeRequirement("daget:bankaccount:write"));
                   });
                options.AddPolicy("ReadBankAccount",
                  policy =>
                  {
                      policy.Requirements.Add(new HaveScopeRequirement("daget:bankaccount:read"));
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

            app.UseExceptionHandler(string.Concat("/", Constants.Routes.error));
            factory.AddConsole(LogLevel.Information);
            app.UseMvc();
        }
    }
}
