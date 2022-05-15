namespace LogWindow.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using LogWindow.Abstractions;

    /// <inheritdoc />
    public class UnionInfoMessage : BaseCompositeMessage<InfoCountMessage>, ICanBeUnion<InfoCountMessage>
    {
        private bool _showCount;

        /// <inheritdoc />
        public UnionInfoMessage(IEnumerable<InfoCountMessage> messages, bool showCount)
            : base(messages)
        {
            Title = Messages.FirstOrDefault()?.ClearMessage;
            _showCount = showCount;
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc/>
        public BaseMessage UnionMessages(InfoCountMessage message)
        {
            Messages.Add(message);
            return this;
        }

        /// <inheritdoc/>
        public bool CanUnionWith(InfoCountMessage message)
        {
            return string.Equals(Title, message.ClearMessage);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _showCount ? $"{Title} : {Messages.Count}" : Title;
        }
    }
}
