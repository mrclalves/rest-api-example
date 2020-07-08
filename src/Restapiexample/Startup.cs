using System;
using JHipsterNet.Config;
using Compuletra.RestApiExample.Data;
using Compuletra.RestApiExample.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

[assembly: ApiController]

namespace Compuletra.RestApiExample {
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
            .AddNhipsterModule(Configuration);

            AddDatabase(services);

            var jhipsterSettings = services.BuildServiceProvider().GetRequiredService<IOptions<JHipsterSettings>>();

            services
            .AddSecurityModule(jhipsterSettings.Value)
            .AddProblemDetailsModule()
            .AddAutoMapperModule()
            .AddWebModule()
            .AddSwaggerModule();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider,
            ApplicationDatabaseContext context, IOptions<JHipsterSettings> jhipsterSettingsOptions)
        {
            var jhipsterSettings = jhipsterSettingsOptions.Value;
            app
                .UseApplicationSecurity(jhipsterSettings)
                .UseApplicationProblemDetails()
                .UseApplicationWeb(env)
                .UseApplicationSwagger()
                .UseApplicationDatabase(serviceProvider, env);    
        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            services.AddDatabaseModule(Configuration);
        }
    }
}
