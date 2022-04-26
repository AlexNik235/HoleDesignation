using System.Xml.Linq;
using Helpers;

namespace Models
{
    /// <inheritdoc />
    public class RevitComponents : Components
    {
        /// <inheritdoc />
        protected override XElement GetComponentEntry()
        {
            return new XElement(
                "ComponentEntry",
                new XAttribute(nameof(ModuleName), ModuleName.Ensure()));
        }
    }
}