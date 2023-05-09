using EasyShoes_TGBot.Models;

namespace EasyShoes_TGBot.Services
{
    public class Functions : IFunctions
    {
        public string Function(string buttonCode, string text)
        {
            // Если нажата кнопка с заказом
            if (buttonCode == AppSettings.BC_newOrder)
            {
                string dateText = "✅ <b>Ваша заявка принята.</b>\n\n";
                if(DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateText += "❗️<b><u>Сегодня эксперт отдыхает</u></b> и ответит вам в рабочее время 😊\n\n";
                }
                return $"{dateText}👍 Спасибо! Скоро с вами свяжемся!";
            }

            // Если нажата кнопка с инструкцией
            /*if (buttonCode == AppSettings.BC_instruction)
            {
                return string.Empty;
            }*/

            if (buttonCode == AppSettings.NewSession)
                return $"Нажмите > /start < для начала работы";

            else return string.Empty;
        }
    }
}

