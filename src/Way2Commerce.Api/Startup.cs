using Way2Commerce.Api.Extensions;
using Way2Commerce.Api.IoC;

namespace Way2Commerce.Api;

public class Startup : Interfaces.IStartup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddVersioning();
        services.AddSwagger();
        services.AddAuthentication(Configuration);
        services.AddAuthorizationPolicies();
        services.RegisterServices(Configuration);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(builder => builder
            .SetIsOriginAllowed(orign => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        app.MapControllers();
    }
}