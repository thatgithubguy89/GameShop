using GameShop.Api.Interfaces;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using GameShop.Api.Models;

namespace GameShop.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IGameRepository _gameRepository;
        private readonly IGameOrderRepository _gameOrderRepository;

        public EmailService(IConfiguration configuration, IGameRepository gameRepository, IGameOrderRepository gameOrderRepository)
        {
            _configuration = configuration;
            _gameRepository = gameRepository;
            _gameOrderRepository = gameOrderRepository;
        }

        public async Task SendEmails(Order order)
        {
            var ids = _gameOrderRepository.GetGameIdsByOrderId(order.Id);

            var games = await _gameRepository.GetGamesByIdsAsync(ids);

            foreach (var game in games)
            {
                SendSingleEmail(game, order);
            }
        }

        private void SendSingleEmail(Game game, Order order)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["SMTP:Sender"]));
            email.To.Add(MailboxAddress.Parse(order.CreatedBy));
            email.Subject = $"Request to review {game.Title}.";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h3>Title: {game.Title}</h3>" +
                       $"<h3>Order Number: {order.OrderNumber}</h3>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["SMTP:Host"], _configuration.GetSection("SMTP:Port").Get<int>(), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["SMTP:Sender"], _configuration["SMTP:SenderPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
