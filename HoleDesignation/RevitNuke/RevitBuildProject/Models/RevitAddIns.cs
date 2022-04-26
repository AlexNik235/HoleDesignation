using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// Specifies Revit addin file.
    /// </summary>
    public class RevitAddIns
    {
        /// <summary>
        /// List of addins.
        /// </summary>
        public List<AddIn>? AddIn { get; set; }
    }
}