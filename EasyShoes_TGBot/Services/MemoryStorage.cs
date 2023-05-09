using System.Collections.Concurrent;
using EasyShoes_TGBot.Models;

namespace EasyShoes_TGBot.Services
{
	public class MemoryStorage : IStorage
	{
        // Хранилище сессий
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // Создаем и возвращаем новую, если такой не было
            var newSession = new Session()
            {
                // Режим по умолчанию для новой сессии
                ButtonCode = AppSettings.NewSession
            };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}

