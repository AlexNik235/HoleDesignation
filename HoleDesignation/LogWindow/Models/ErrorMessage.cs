namespace LogWindow.Models
{
    using System;
    using Abstractions;

    /// <inheritdoc cref="PikTools.LogWindow.Abstractions.BaseLinkedElementMessage" />
    public class ErrorMessage : BaseLinkedElementMessage, ICanBeUnion<ErrorMessage>
    {
        /// <inheritdoc />
        public ErrorMessage(string text, string element, IBaseObject elementId)
            : base(text, elementId, element)
        {
        }

        /// <inheritdoc />
        public BaseMessage UnionMessages(ErrorMessage message)
        {
            if (!CanUnionWith(message))
            {
                throw new ArgumentException("Невозможно объединить сообщения");
            }

            return new ErrorMessageMany(new[] { this, message });
        }

        /// <inheritdoc />
        public bool CanUnionWith(ErrorMessage message)
        {
            return string.Equals(Text, message.Text);
        }
    }
}
