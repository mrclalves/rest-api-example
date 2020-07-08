using Microsoft.AspNetCore.Builder;
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

        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder @this)
        {
            @this.UseSwagger();
            @this.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Restapiexample API");
            });
            return @this;
        }
    }
}
