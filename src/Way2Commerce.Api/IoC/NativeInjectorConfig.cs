using Microsoft.EntityFrameworkCore;
using Way2Commerce.Data.Context;
using Way2Commerce.Data.Repositories;
using Way2Commerce.Domain.Interfaces.Repositories;
using Way2Commerce.Domain.Interfaces.Services;
using Way2Commerce.Domain.Services;

namespace Way2Commerce.Api.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Way2CommerceConnection"))
            );
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}