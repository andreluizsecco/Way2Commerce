using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Way2Commerce.Api.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Way2Commerce.Api",
                    Version = "v1"
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Way2Commerce.Api",
                    Version = "v2"
                });
            });
        }

        public static void UseSwaggerUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
                if (apiVersionProvider == null)
                    throw new ArgumentException("API Versioning not registered.");
                    
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName);
                }
                options.RoutePrefix = string.Empty;

                options.DocExpansion(DocExpansion.List);
            });
        }
    }
}