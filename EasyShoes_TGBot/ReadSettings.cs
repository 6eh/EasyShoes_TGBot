using System.Xml;
using System.Xml.Linq;
using EasyShoes_TGBot.Models;

namespace EasyShoes_TGBot
{
	public class ReadSettings
	{
        static public void ReadXMLSettings()
		{
            string path = AppSettings.SettingsFile;

            if(!File.Exists(path))
            {
                Message($"Не найден фаил настроек:\n" +
                    $"{Path.GetFullPath(path)}\n");

                if (AppSettings.GenerateXMLFile())
                    Message("Фаил настроек создан, заполните его и перезапустите программу!", true);
                else
                    Message("Не удалось создать фаил настроек, создайте его самостоятельно по инструкции на GitHub этой программы");
            }

            XDocument doc = XDocument.Load(path);
            AppSettings.TelegramBotToken = doc.Root.Element("TelegramBotToken").Value;
            string adminIdS = doc.Root.Element("AdminId").Value;
            long adminIdL;
            if (long.TryParse(adminIdS, out adminIdL))
            {
                AppSettings.AdminId = adminIdL;
            }
            else
            {
                Message($"Ошибка в настройках Id админа {adminIdS} не конвертируется в long!\n" +
                    $"Проверьте фаил настроек:\n" +
                    $"{Path.GetFullPath(path)}", true);
            }
            string superAdminIdS = doc.Root.Element("SuperAdminId").Value;
            long superAdminIdL;
            if (long.TryParse(superAdminIdS, out superAdminIdL))
            {
                AppSettings.SuperAdminId = superAdminIdL;
            }
            else
            {
                Message($"Ошибка в настройках Id супер-админа {superAdminIdS} не конвертируется в long!\n" +
                    $"Проверьте фаил настроек:\n" +
                    $"{Path.GetFullPath(path)}", true);
            }

            AppSettings.AdminUserName = doc.Root.Element("AdminUserName").Value;
            AppSettings.VideoInstruction = doc.Root.Element("VideoInstruction").Value;

            Console.WriteLine("SuperAdminId: " + AppSettings.SuperAdminId);
            Console.WriteLine("AdminId: " + AppSettings.AdminId);
            Console.WriteLine("AdminUserName: " + AppSettings.AdminUserName);
            Console.WriteLine("VideoInstruction:" + AppSettings.VideoInstruction);
        }

        public static void Message(string text, bool exitProgram = false)
        {
            Console.WriteLine(text);
            if(exitProgram)
            {
                Console.WriteLine($"\nНажмите любую клавишу для выхода из программы...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
	}
}

