using GameShop.Repositories;
using GameShop.Services;

namespace GameShop.Extensions
{
    public static class ProgramExtend
    {
        public static WebApplicationBuilder AddRepositoriesAndServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            return builder;
        }
    }
}
