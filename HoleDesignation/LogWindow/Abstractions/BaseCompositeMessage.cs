namespace LogWindow.Abstractions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <inheritdoc />
    public abstract class BaseCompositeMessage<T> : BaseMessage
        where T : BaseMessage
    {
        /// <inheritdoc />
        public BaseCompositeMessage(IEnumerable<T> messages)
        {
            Messages = messages.ToList();
        }

        /// <summary>
        /// Сообщения
        /// </summary>
        protected List<T> Messages { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            foreach (var message in Messages)
            {
                sb.AppendLine(message.ToString());
            }

            return sb.ToString();
        }
    }
}
