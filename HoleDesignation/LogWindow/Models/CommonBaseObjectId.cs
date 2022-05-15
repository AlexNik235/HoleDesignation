namespace LogWindow.Models
{
    using Abstractions;

    /// <summary>
    /// Представляет базовый тип для Revit.
    /// </summary>
    public class CommonBaseObjectId : IBaseObject
    {
        private readonly long _id;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CommonBaseObjectId"/>.
        /// </summary>
        /// <param name="elementId">Id элемента.</param>
        public CommonBaseObjectId(long elementId)
        {
            DescObject = elementId;
            _id = elementId;
        }

        /// <inheritdoc />
        public object DescObject { get; }

        /// <inheritdoc />
        public override string ToString() => _id.ToString();

        /// <inheritdoc />
        public long GetId() => _id;

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var objId = ((CommonBaseObjectId)obj).GetId();

            if (objId < _id)
                return 1;
            if (objId < _id)
                return -1;
            return 0;
        }
    }
}
