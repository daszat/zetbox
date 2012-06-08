
namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This attribute is used to map from a class or property definition back to the instance that defined it.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class DefinitionGuidAttribute : Attribute
    {
        /// <summary>
        /// Save the Guid as string for minimal parsing overhead
        /// </summary>
        private readonly string guid;

        public DefinitionGuidAttribute(string guid)
        {
            this.guid = guid;
        }

        public Guid Guid
        {
            get { return new Guid(guid); }
        }
    }
}