namespace Way2Commerce.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder builder) where TStartup : Interfaces.IStartup
    {
        var startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration) as Interfaces.IStartup;
        if (startup == null)
            throw new ArgumentException("Startup class is invalid.");
        
        startup.ConfigureServices(builder.Services);
        var app = builder.Build();
        startup.Configure(app, app.Environment);
        app.Run();

        return builder;
    } 
}