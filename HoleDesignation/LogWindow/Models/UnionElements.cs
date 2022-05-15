namespace LogWindow.Models
{
    using System.Collections.Generic;
    using Abstractions;

    /// <summary>
    /// Группа элементов с общим именем
    /// </summary>
    public class UnionElements
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="elementName">Общее имя элементов</param>
        /// <param name="ids">Идентификаторы элементов</param>
        public UnionElements(string elementName, List<IBaseObject> ids)
        {
            ElementName = elementName;
            Ids = ids;
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Идентификаторы элементов
        /// </summary>
        public List<IBaseObject> Ids { get; }
    }
}
