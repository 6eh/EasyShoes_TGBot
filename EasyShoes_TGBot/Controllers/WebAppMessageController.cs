using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EasyShoes_TGBot.Controllers
{
	public class WebAppMessageController
	{
        private readonly ITelegramBotClient _telegramClient;

        public WebAppMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                $"Получено сообщение в WebAppMessageController:" +
                $"{message.WebAppData.Data}", cancellationToken: ct);
            if (message.WebAppData != null)
            {
                Console.WriteLine($"Получена Вебдата (WebAppMessageController)");
            }
        }
    }
}

