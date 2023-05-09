using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using EasyShoes_TGBot.Services;
using EasyShoes_TGBot.Models;
using System.Threading;

namespace EasyShoes_TGBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IFunctions _functions;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IFunctions functions)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _functions = functions;
        }


        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");

            string htmlPath = @"https://tunedevice.ru/easyshoes/easyshoes.html";

            //if (message.Text is not { } messageText)
            //  return;
            if (message.Text is { } messageText)
            {
                //Console.WriteLine($"{message.Text}");

                // тест открытия сайтов через WebApp
                if (message.Text.Contains("/site:"))
                {
                    string url = message.Text.Replace("/site:", "https://");
                    WebAppInfo webAppInfo = new WebAppInfo();
                    webAppInfo.Url = url;

                    KeyboardButton keyboardButton = new KeyboardButton(text: "Перейти на сайт");
                    keyboardButton.WebApp = webAppInfo;

                    ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(keyboardButton) { ResizeKeyboard = true };

                    Message sentMessage = await _telegramClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Нажми кнопку",
                        replyMarkup: reply,
                        cancellationToken: ct);
                }

                switch (message.Text)
                {
                    case "/start":
                        await GetMenu(message, ct);
                        break;

                    case "/id":
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>Ваш ID</b>: {message.Chat.Id}", parseMode: ParseMode.Html ,cancellationToken: ct);
                        break;

                    // Кнопка внизу (клавиатура)
                    case "/webapp":
                        WebAppInfo webAppInfo = new WebAppInfo();
                        webAppInfo.Url = htmlPath;

                        KeyboardButton keyboardButton = new KeyboardButton(text: "Сделать заказ");
                        keyboardButton.WebApp = webAppInfo;

                        ReplyKeyboardMarkup reply = new ReplyKeyboardMarkup(keyboardButton) { ResizeKeyboard = true };

                        Message sentMessage = await _telegramClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Нажми кнопку",
                            replyMarkup: reply,
                            cancellationToken: ct);

                        break;

                    // Инлайн кнопка (в чате)
                    case "/webapp2":
                        WebAppInfo webAppInfo1 = new WebAppInfo();
                        webAppInfo1.Url = htmlPath;

                        InlineKeyboardButton inlKeyboardButton = new (text: "Сделать заказ");
                        inlKeyboardButton.WebApp = webAppInfo1;

                        Message sentMessage1 = await _telegramClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Choose a response",
                            replyMarkup: new InlineKeyboardMarkup(inlKeyboardButton),
                            cancellationToken: ct);

                        break;

                    case "/newsettings":
                        // Обновить настройки из xml файла если это супер-админ
                        if (message.Chat.Id == AppSettings.SuperAdminId)
                        {
                            ReadSettings.ReadXMLSettings();
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Настройки обновлены:\n" +
                            $"SuperAdminId: {AppSettings.SuperAdminId}\n" +
                            $"AdminId: {AppSettings.AdminId}\n" +
                            $"AdminUserName: {AppSettings.AdminUserName}\n" +
                            $"VideoInstruction: {AppSettings.VideoInstruction}",
                            parseMode: ParseMode.Html, cancellationToken: ct);
                        }                        
                        break;

                    default:
                        string buttonCode = _memoryStorage.GetSession(message.Chat.Id).ButtonCode;
                        var result = _functions.Function(buttonCode, message.Text);

                        if (buttonCode == AppSettings.BC_newOrder)
                        {
                            // Вывод сообщения "Спасибо за заказ"
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, parseMode: ParseMode.Html ,cancellationToken: ct);
                            // Передача информации админу
                            string orderMessage = $"Пользователь:\n" +
                                $" ID: {message.Chat.Id}\n" +
                                $" First Name: {message.Chat.FirstName}\n" +
                                $" Last Name: {message.Chat.LastName}\n" +
                                $" TG User Name: {message.Chat.Username}\n" +
                                $"Отправил в бот сообщение:\n" +
                                $"{message.Text}";
                            await _telegramClient.SendTextMessageAsync(AppSettings.AdminId, orderMessage, cancellationToken: ct);
                            Console.WriteLine($"\nНовое сообщение:\n{orderMessage}\n---\n");

                            Logging(orderMessage);
                        }
                        else
                            await GetMenu(message, ct);

                        break;
                }
                // Сброс сессии после обработки
                ResetSession(message.Chat.Id);
            }

            if (message is WebAppData )
            {
                Console.WriteLine($"Получена Вебдата");
            }
        }

        // Сброс сессии
        private void ResetSession(long chatId)
        {
            _memoryStorage.GetSession(chatId).ButtonCode = AppSettings.NewSession;
        }

        public async Task GetMenu(Message message, CancellationToken ct)
        {
            var buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData($"✅️ Создать заказ", $"{AppSettings.BC_newOrder}"),
                InlineKeyboardButton.WithCallbackData($"⚠️ Как измерить стопу", $"{AppSettings.BC_instruction}"),
            });

            // Передаем кнопки вместе с сообщением (параметр ReplyMarkup)
            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                $"<b>Связь с админом: {AppSettings.AdminUserName}</b>",
                cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
        }

        private static async Task Logging(string text)
        {
            using (StreamWriter sr = new StreamWriter(AppSettings.OrderLogFile, true))
            {
                try
                {
                    await sr.WriteLineAsync($"\n{DateTime.Now}:\n{text}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"\n{DateTime.Now}: Logging error!\n{ex.Message}");
                }
            }
        }
    }
}

