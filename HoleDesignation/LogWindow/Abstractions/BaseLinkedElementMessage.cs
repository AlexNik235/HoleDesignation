namespace LogWindow.Abstractions
{
    /// <summary>
    /// Базовый класс, описывающий ошибку для журнала.
    /// </summary>
    public abstract class BaseLinkedElementMessage : BaseMessage
    {
        /// <inheritdoc />
        protected BaseLinkedElementMessage(string text, IBaseObject elementId, string element)
        {
            Text = text;
            ElementId = elementId;
            Element = element;
        }

        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Id элемента.
        /// </summary>
        public IBaseObject ElementId { get; }

        /// <summary>
        /// Имя элемента.
        /// </summary>
        public string Element { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Text} {Element} {(ElementId is null ? string.Empty : ("Id = " + ElementId))}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ElementId != null ? ElementId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Element != null ? Element.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((BaseLinkedElementMessage)obj);
        }

        /// <inheritdoc />
        private bool Equals(BaseLinkedElementMessage other)
        {
            return Text == other.Text && Equals(ElementId, other.ElementId) && Element == other.Element;
        }
    }
}
