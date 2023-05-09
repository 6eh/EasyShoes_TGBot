using Telegram.Bot;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EasyShoes_TGBot.Controllers;
using EasyShoes_TGBot.Services;
using EasyShoes_TGBot;
using System.Xml.Linq;
using System.Xml;
using EasyShoes_TGBot.Models;

ReadAppSettings();


// Объект, отвечающий за постоянный жизненный цикл приложения
var host = new HostBuilder()
    .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
    .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
    .Build();// Собираем

Console.WriteLine("Сервис запущен");
// Запускаем сервис
await host.RunAsync();
Console.WriteLine("Сервис остановлен");

static void ConfigureServices(IServiceCollection services)
{
    // Подключаем контроллеры сообщений и кнопок
    services.AddTransient<TextMessageController>();
    services.AddTransient<DefaultMessageController>();
    services.AddTransient<WebAppMessageController>();
    services.AddTransient<InlineKeyboardController>();
    // Регистрируем объект TelegramBotClient c токеном подключения
    services.AddSingleton<ITelegramBotClient>(provider
        => new TelegramBotClient(AppSettings.TelegramBotToken));
    // Регистрация сервиса сессии
    services.AddSingleton<IStorage, MemoryStorage>();

    services.AddSingleton<IFunctions, Functions>();

    // Регистрируем постоянно активный сервис бота
    services.AddHostedService<Bot>();

    
}

static void ReadAppSettings()
{
    ReadSettings.ReadXMLSettings();    
}