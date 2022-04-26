namespace Extensions
{
    using System;
    using Models;

    /// <summary>
    /// The <see cref="string"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets type name of a <see cref="PluginType"/>.
        /// </summary>
        /// <param name="type">The plugin type name.</param>
        public static PluginType ToPluginType(this string type)
        {
            return type switch
            {
                Constants.Cmd => PluginType.Command,
                Constants.AppCmd => PluginType.Application,
                _ => throw new NotSupportedException()
            };
        }
    }
}