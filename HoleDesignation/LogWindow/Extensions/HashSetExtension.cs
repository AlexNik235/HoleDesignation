namespace LogWindow.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Методы расширения для <see cref="HashSet{T}"/>
    /// </summary>
    public static class HashSetExtension
    {
        /// <summary>
        /// Добавить элемент в hashSet
        /// </summary>
        /// <param name="set">HashSet</param>
        /// <param name="item">Item</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns></returns>
        public static bool TryAdd<T>(this HashSet<T> set, T item)
        {
            if (set.Contains(item))
                return false;
            set.Add(item);
            return true;
        }
    }
}
