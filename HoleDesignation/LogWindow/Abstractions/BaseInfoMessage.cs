namespace LogWindow.Abstractions
{
    /// <summary>
    /// Базовый класс для информационного сообщения
    /// </summary>
    public abstract class BaseInfoMessage : BaseMessage
    {
        private readonly string _message;

        /// <inheritdoc />
        public BaseInfoMessage(string message)
        {
            _message = message;
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
            return Equals((BaseInfoMessage)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (_message != null ? _message.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _message;
        }
        
        /// <inheritdoc />
        protected bool Equals(BaseInfoMessage other)
        {
            return _message == other._message;
        }
    }
}
