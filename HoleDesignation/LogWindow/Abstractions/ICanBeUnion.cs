namespace LogWindow.Abstractions
{
    /// <summary>
    /// Позволяет объединиться
    /// </summary>
    /// <typeparam name="T"><see cref="BaseMessage"/>></typeparam>
    public interface ICanBeUnion<T>
        where T : BaseMessage
    {
        /// <summary>
        /// Объединить сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Возвращает объединенное сообщение</returns>
        public BaseMessage UnionMessages(T message);

        /// <summary>
        /// Проверка, можно ли объединить с заданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public bool CanUnionWith(T message);
    }
}
