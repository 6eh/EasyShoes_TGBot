using System;
using System.Threading;
using EasyShoes_TGBot.Models;
using EasyShoes_TGBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EasyShoes_TGBot.Controllers
{
	public class InlineKeyboardController
	{
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).ButtonCode = callbackQuery.Data;

            string buttonCode = callbackQuery.Data;

            if(buttonCode == AppSettings.BC_newOrder)
            {
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                    $"🦶<b>Приветвуем в Easy Shoes!</b>🦶\n" +
                    $"Напишите пожалуйста <b><u>одним сообщением</u></b> информацию для заказа:\n\n" +
                    $"👉 Ваш номер телефона;\n" +
                    $"👉 Длину стопы в см;\n" +
                    $"👉 Ширину стопы в см;\n" +
                    $"👉 Пол ребенка;\n" +
                    $"{Environment.NewLine}⁉️Если есть вопросы, просто напишите их сюда (<b><u>обязательно оставьте номер телефона для связи</u></b>).",
                    cancellationToken: ct, parseMode: ParseMode.Html);
            }

            if (buttonCode == AppSettings.BC_instruction)
            {
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                    $"<b>Вот видеоинструкция:</b>",
                    cancellationToken: ct, parseMode: ParseMode.Html);

                await _telegramClient.SendVideoAsync(
                    chatId: callbackQuery.From.Id,
                    video: AppSettings.VideoInstruction,
                    supportsStreaming: true,
                    cancellationToken: ct);

                // Сброс сессии
                buttonCode = AppSettings.NewSession;
            }

            /*
            // Генерим информационное сообщение
            string buttonCode = callbackQuery.Data switch
            {
                "newOrder" => "Тут инфа о заказе",
                "instruction" => "Тут видео инструкция",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбран режим - </b>{buttonCode}.{Environment.NewLine}",
                cancellationToken: ct, parseMode: ParseMode.Html);
            */
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
        }
    }
}

