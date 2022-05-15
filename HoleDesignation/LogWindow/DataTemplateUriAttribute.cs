namespace LogWindow
{
    using System;

    /// <summary>
    /// Ссылка на DataTemplate для сообщений
    /// </summary>
    public class DataTemplateUriAttribute : Attribute
    {
        /// <inheritdoc />
        public DataTemplateUriAttribute(string uri, string key, Type type)
        {
            Key = key;
            Type = type;
            Uri = new Uri(uri, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Ключ в словаре
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Type
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Uri
        /// </summary>
        public Uri Uri { get; }
    }
}
