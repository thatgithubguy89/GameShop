using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmails(Order order);
    }
}
