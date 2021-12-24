using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Way2Commerce.Api.Interfaces;

public interface IStartup
{
    IConfiguration Configuration { get; }
    void ConfigureServices(IServiceCollection services);
    void Configure(WebApplication app, IWebHostEnvironment env);
}