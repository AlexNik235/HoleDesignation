namespace Models
{
    /// <summary>
    /// Specifies the type from an assembly.
    /// </summary>
    public class AssemblyType
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="AssemblyType"/> class.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="fullName">The assembly full name.</param>
        /// <param name="baseTypeName">The assembly base type name.</param>
        public AssemblyType(string assemblyName, string fullName, string? baseTypeName)
        {
            AssemblyName = assemblyName;
            FullName = fullName;
            BaseTypeName = baseTypeName;
        }

        /// <summary>
        /// The assembly name.
        /// </summary>
        public string AssemblyName { get; } = string.Empty;

        /// <summary>
        /// The assembly full name.
        /// </summary>
        public string FullName { get; } = string.Empty;

        /// <summary>
        /// The assembly base type name.
        /// </summary>
        public string? BaseTypeName { get; } = string.Empty;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{FullName} : {BaseTypeName}";
        }

        /// <summary>
        /// Тип плагина
        /// </summary>
        public string PluginType { get; set; } = string.Empty;
    }
}