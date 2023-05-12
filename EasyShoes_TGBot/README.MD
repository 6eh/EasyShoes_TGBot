## Для работы бота требуется XML файл с настройками

Код файла настроек:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Root>
    <TelegramBotToken>PASTE_YOUR_BOT_TOKEN_HERE</TelegramBotToken>
    <SuperAdminId>PASTE_SUPER_ADMIN_TELEGRAM-ID_HERE</SuperAdminId>
    <AdminId>PASTE_ADMIN_TELEGRAM-ID_HERE</AdminId>  
    <AdminUserName>PASTE_ADMIN_TELEGRAM-@USERNAME_HERE</AdminUserName>
    <VideoInstruction>PASTE_HTTPS-ADRESS_YOUR_INSTRUCTION</VideoInstruction>
</Root>
```
Программа создаст его в своей папке первом запуске.

Если по каким-то причинам программа не сможет его создать, создайте файл самостоятельно:
 - Создайте xml фаил в папке с программой;
 - Назовите фаил "settings" (settings.xml);
 - Убедитесь, что разрешение файла - "xml";
 - Заполните файл, как показано выше ("Код файла настроек";)

Далее необходимо заполнить настройки:
 - TelegramBotToken - Токен вашего Телеграм бота;
 - SuperAdminId - Telegram id Главного администратора (может менять настройки бота);
 - AdminId - Telegram id Администратора (которому приходят заказы из бота);
 - AdminUserName - Имя пользователя (@username) Telegram Администратора (для связи);
 - VideoInstruction - Публичный адрес для показа какой-то инструкции пользователю;

Пример корректного файла настроек:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Root>
    <TelegramBotToken>6883248388:AAEcDh3CxOveS3pSjPq8DVBAax87pA_Yc8</TelegramBotToken>
    <SuperAdminId>888888888</SuperAdminId>
    <AdminId>999999999</AdminId>    
    <AdminUserName>@AdministaratorPetrov</AdminUserName>
    <VideoInstruction>https://t.me/IQKNqSVroVBhYWY6/226"</VideoInstruction>
</Root>
```