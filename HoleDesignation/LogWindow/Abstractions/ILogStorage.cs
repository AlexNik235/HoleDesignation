namespace LogWindow.Abstractions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Представляет тип, который используется для ведения журнала ошибок.
    /// </summary>
    public interface ILogStorage
    {
        /// <summary>
        /// Событие, возникающие при изменении коллекции элементов хранилища.
        /// </summary>
        event EventHandler ElementStorageChanged;

        /// <summary>
        /// Добавить сообщение.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <typeparam name="T">Тип сообщения</typeparam>
        public void AddMessage<T>(T message)
            where T : BaseMessage;

        /// <summary>
        /// Получить коллекцию ошибок.
        /// </summary>
        IEnumerable<BaseMessage> GetMessages();

        /// <summary>
        /// Указывает если ли ошибки в хранилище.
        /// </summary>
        /// <returns></returns>
        bool HasMessages();

        /// <summary>
        /// Очищает хранилище.
        /// </summary>
        void Clear();
    }
}
