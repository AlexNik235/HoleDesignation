namespace LogWindow.Models
{
    using System;
    using System.Collections.Generic;
    using LogWindow.Abstractions;

    /// <inheritdoc />
    public class InfoCountMessage : BaseInfoMessage, ICanBeUnion<InfoCountMessage>
    {
        private bool _showCount;

        /// <inheritdoc />
        public InfoCountMessage(string message, bool showCount = false)
            : base(message)
        {
            _showCount = showCount;
            ClearMessage = message;
        }

        /// <summary>
        /// Сообщение без количества
        /// </summary>
        public string ClearMessage { get; }

        /// <inheritdoc/>
        public bool CanUnionWith(InfoCountMessage message)
        {
            return string.Equals(ClearMessage, message.ClearMessage);
        }

        /// <inheritdoc/>
        public BaseMessage UnionMessages(InfoCountMessage message)
        {
            if (!CanUnionWith(message))
            {
                throw new ArgumentException("Невозможно объединить сообщения");
            }

            return new UnionInfoMessage(new List<InfoCountMessage> { this, message }, _showCount);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _showCount ? $"{base.ToString()} : 1" : base.ToString();
        }
    }
}
