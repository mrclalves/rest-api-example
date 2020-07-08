using Hellang.Middleware.ProblemDetails;
using Compuletra.RestApiExample.Web.Rest.Problems;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Compuletra.RestApiExample.Infrastructure {
    public static class ProblemDetailsStartup {
        //TODO Understand difference between UI and non-ui Exceptions
        //https://github.com/christianacca/ProblemDetailsDemo/blob/master/src/ProblemDetailsDemo.Api/Startup.cs

        public static IServiceCollection AddProblemDetailsModule(this IServiceCollection @this)
        {
            @this.ConfigureOptions<ProblemDetailsConfiguration>();
            @this.AddProblemDetails();

            return @this;
        }

        public static IApplicationBuilder UseApplicationProblemDetails(this IApplicationBuilder @this)
        {
            @this.UseProblemDetails();
            return @this;
        }
    }
}
