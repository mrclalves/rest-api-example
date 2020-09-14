using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Compuletra.RestApiExample.Infrastructure {
    public static class SwaggerConfiguration {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection @this)
        {
            @this.AddSwaggerGen(c => {
                c.SwaggerDoc("v2", new OpenApiInfo {Title = "Restapiexample API", Version = "0.0.1"});
            });

            return @this;
        }

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder @this, IConfiguration configuration)
        {
            @this.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/api-docs";
                c.SerializeAsV2 = true;
            });
            @this.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/v2/api-docs", configuration["spring:application:name"]);
                c.RoutePrefix = string.Empty;
            });
            return @this;
        }
    }
}
