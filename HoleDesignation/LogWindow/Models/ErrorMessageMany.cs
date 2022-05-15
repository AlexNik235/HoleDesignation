namespace LogWindow.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;

    /// <inheritdoc />
    public class ErrorMessageMany : BaseCompositeMessage<ErrorMessage>, ICanBeUnion<ErrorMessage>
    {
        /// <inheritdoc />
        public ErrorMessageMany(IEnumerable<ErrorMessage> messages)
            : base(messages)
        {
            Title = Messages.FirstOrDefault()?.Text;
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Элементы
        /// </summary>
        public List<UnionElements> ElementsList => Messages
            .GroupBy(m => m.Element)
            .Select(g => new UnionElements(
                g.Key,
                g.Select(m => m.ElementId)
                .OrderBy(x => x)
                .ToList()))
            .ToList();

        /// <inheritdoc />
        public BaseMessage UnionMessages(ErrorMessage message)
        {
            Messages.Add(message);
            return this;
        }

        /// <inheritdoc />
        public bool CanUnionWith(ErrorMessage message)
        {
            return string.Equals(Title, message.Text);
        }
    }
}
