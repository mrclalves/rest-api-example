using System;
using JHipsterNet.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Compuletra.RestApiExample.Infrastructure {
    public static class SecurityStartup {


        public static IServiceCollection AddSecurityModule(this IServiceCollection @this, JHipsterSettings jhipsterSettings) 
        {
            @this.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = jhipsterSettings.Security.Authentication.OAuth2.Provider.IssuerUri;
                options.ClientId = jhipsterSettings.Security.Authentication.OAuth2.Provider.ClientId;
                options.ClientSecret = jhipsterSettings.Security.Authentication.OAuth2.Provider.ClientSecret;
                options.SaveTokens = true;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.RequireHttpsMetadata = false; // dev only
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.CallbackPath = new PathString("/login/oauth2/code/oidc");
                options.ClaimActions.MapJsonKey("role", "roles", "role");
                options.ClaimActions.MapJsonKey("role", "groups", "role");
            });

            @this.AddAuthorization();
            return @this;            
        }

        public static IApplicationBuilder UseApplicationSecurity(this IApplicationBuilder @this,
            JHipsterSettings jhipsterSettings)
        {
            @this.UseCors(CorsPolicyBuilder(jhipsterSettings.Cors));            
            @this.UseAuthentication();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            @this.UseHsts();
            @this.UseHttpsRedirection();
            return @this;
        }

        private static Action<CorsPolicyBuilder> CorsPolicyBuilder(Cors config)
        {
            //TODO implement an url based cors policy rather than global or per controller
            return builder => {
                if (!config.AllowedOrigins.Equals("*"))
                {
                    if (config.AllowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                    else
                    {
                        builder.DisallowCredentials();
                    }
                }

                builder.WithOrigins(config.AllowedOrigins)
                    .WithMethods(config.AllowedMethods)
                    .WithHeaders(config.AllowedHeaders)
                    .WithExposedHeaders(config.ExposedHeaders)
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(config.MaxAge));
            };
        }
    }
}
