using EasyShoes_TGBot.Models;

namespace EasyShoes_TGBot.Services
{
	public interface IStorage
	{
        // Получение сессии
        Session GetSession(long chatId);
	}
}

