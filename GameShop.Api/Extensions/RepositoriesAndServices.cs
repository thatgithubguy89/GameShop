using GameShop.Api.Interfaces;
using GameShop.Api.Repositories;
using GameShop.Api.Services;
using Stripe;

namespace GameShop.Api.Extensions
{
    public static class RepositoriesAndServices
    {
        public static IServiceCollection AddRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameOrderRepository, GameOrderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IPageService<,>), typeof(PageService<,>));
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<CustomerService>();
            services.AddScoped<ChargeService>();
            services.AddScoped<TokenService>();

            return services;
        }
    }
}
