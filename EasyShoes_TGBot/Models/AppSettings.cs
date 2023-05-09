using System;
namespace EasyShoes_TGBot.Models
{
	public static class AppSettings
	{
        readonly public static string SettingsFile = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}settings.xml";
        public static string TelegramBotToken { get; set; }
        public static long AdminId { get; set; }
        public static long SuperAdminId { get; set; }
        public static string AdminUserName { get; set; }
        public static string BC_newOrder { get; set; } = "newOrder";
        public static string BC_instruction { get; set; } = "instruction";
        public static string NewSession { get; set; } = "newSession";
        public static string VideoInstruction { get; set; }
        public static string OrderLogFile { get; set; } = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}OrdersLog.txt";

        public static bool GenerateXMLFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(SettingsFile))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    sw.WriteLine("<Root>");
                    sw.WriteLine("<TelegramBotToken>PASTE_YOUR_BOT_TOKEN_HERE</TelegramBotToken>");
                    sw.WriteLine("<SuperAdminId>PASTE_SUPER_ADMIN_TELEGRAM-ID_HERE</SuperAdminId>");
                    sw.WriteLine("<AdminId>PASTE_ADMIN_TELEGRAM-ID_HERE</AdminId>  ");
                    sw.WriteLine("<AdminUserName>PASTE_ADMIN_TELEGRAM-@USERNAME_HERE</AdminUserName>");
                    sw.WriteLine("<VideoInstruction>PASTE_HTTPS-ADRESS_YOUR_INSTRUCTION</VideoInstruction>");
                    sw.WriteLine("</Root>");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}