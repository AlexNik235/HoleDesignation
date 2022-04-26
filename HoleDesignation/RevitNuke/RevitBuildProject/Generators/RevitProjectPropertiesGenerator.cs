namespace Generators
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Extensions;
    using Models;
    using Nuke.Common.ProjectModel;

    /// <inheritdoc />
    public class RevitProjectPropertiesGenerator : ProjectPropertiesGenerator
    {
        /// <inheritdoc />
        protected override IEnumerable<XElement> GenerateAdditionalProperties(
            Project project,
            IEnumerable<AssemblyType> pluginTypes)
        {
            foreach (var type in pluginTypes)
            {
                var propertyName = type.ToPropertyName();
                var property = project.GetProperty(propertyName);
                Console.Write($"\n__________________________________________________");
                Console.Write($"\n{propertyName}");
                if (property == null)
                {
                    yield return new XElement(propertyName, Guid.NewGuid());
                }
            }
        }
    }
}