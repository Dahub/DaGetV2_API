using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaGetCore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNet.Security.OAuth.Introspection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DaGetCore.WebApi
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
            services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));


            services.AddLogging();


            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                // options.DefaultChallengeScheme = OAuthIntrospectionDefaults.AuthenticationScheme;                
            }).AddOAuthIntrospection(options =>
            {
                options.Configuration = new OAuthIntrospectionConfiguration()
                {
                    IntrospectionEndpoint = "http://localhost:63293/introspect"
                };

                options.IncludeErrorDetails = true;
                options.Audiences.Add("DaGet");
                options.ClientId = "_kZ2#412#Edcm-5f";
                options.ClientSecret = "og3Rkf--red###2";
                options.RequireHttpsMetadata = false;
                //options.Events = new OAuthIntrospectionEvents()
                //{
                //    OnCreateTicket = context =>
                //    {
                //        c
                //        context.HttpContext.User.AddIdentity(context.Identity);                     

                //        return Task.FromResult(0);
                //    },
                //    OnRetrieveToken = context =>
                //    {
                //        return Task.FromResult(0);
                //    },
                //    OnValidateToken = context =>
                //    {
                //        return Task.FromResult(0);
                //    },
                //    OnSendIntrospectionRequest = context =>
                //    {
                //        return Task.FromResult(0);
                //    }
                //};
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateBankAccount",
                    policy =>
                    {
                        policy.Requirements.Add(new HaveScopeRequirement("daget:bankaccount:rw"));                        
                    });
            });
        }

        public void Testtruc(CreateTicketContext context)
        {
            var tt = context;
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
